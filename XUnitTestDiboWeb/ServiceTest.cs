using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using DiboWeb.Models;
using DiboWeb.Service;
using Newtonsoft.Json;
using DiboWeb.Helper;

namespace XUnitTestDiboWeb
{
    public class ServiceTest :IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly UserService userService;
        private ProjectService projectService;
        private GxDbContext gxDb;
        private PropertyService propertyService;

        public ServiceTest(ITestOutputHelper output)
        {
            this.output = output;

            gxDb = new GxContextFactory().CreateDbContext(new string[] { "test" });
            userService = new UserService( gxDb );
            projectService = new ProjectService(gxDb);
            propertyService = new PropertyService(gxDb);

            // add property
            var data = TemplateConfig.ReadDefaultTemplate();
            propertyService.AddProperties(data);
            //add  template
            var template = new PropertyTemplate() { Name = "Default" };
            //add template-property
            List<Template_Property> template_s = new List<Template_Property>(data.Count);
            foreach (var item in data)
            {
                template_s.Add(new Template_Property() { PropertyTemplate = template, GxProperty = item });
            }
            template.Template_Properties = template_s;

            gxDb.PropertyTemplates.Add(template);
            gxDb.SaveChanges();

            //add user data
            User[] users = new User[]
            {
                new User(){Name= "Jack",PassWord="123" },
                new User(){Name= "Andy",PassWord="123"},
                new User(){Name= "Frank",PassWord ="123"},
                new User(){Name= "张山", PassWord= "zhangshan" }
            };
            foreach (var user in users)
            {
                if(userService.Register(user.Name, user.PassWord))
                {
                    output.WriteLine($"{user.Name}注册成功!");
                }
                else
                {
                    output.WriteLine($"{user.Name}注册失败,用户名已存在!");
                }
            }
            //add project
            User u = userService.Login("Jack", "123");
            Project project = new Project() { Creator = u, Name = "Test Project1" };
            project.PropertyTemplate = template;
            
            userService.CreateProject(u, ref project);
        }
        [Fact]
        public void LoginTest()
        {
            User user = userService.Login("张山", "zhangshan");

            Assert.NotNull(user);
        }
        [Fact]
        public void TestAddProject()
        {
            User user = userService.Login("Jack", "123");
            Project project = new Project() { Creator = user, Name = "Test Project2" };
            userService.CreateProject(user, ref project);

            User u2 = userService.GetUser("张山");
            projectService.AddUser(project,u2);
            projectService.LoadProjectData(ref project);
            Assert.Equal(2, project.UserProjects.Count);
        }
        [Fact]
        public void DeleteProject()
        {
            User user = userService.Login("Jack", "123");
            var up = userService.GetUserProjects(user);
            foreach (var item in up)
            {
                userService.DeleteProject(user, item.Project);
            }
            var up2 = userService.GetUserProjects(user);
            Assert.Equal(1, up2.Count);
        }
        [Fact]
        public void ActiveProject()
        {
            User user = userService.Login("张山", "zhangshan");
            Project project1 = new Project() { Creator = user, Name = "Test Project2" };
            Project project2 = new Project() { Creator = user, Name = "Test Project3" };

            userService.CreateProject(user, ref project1);

            userService.GetActiveProject(user, out Project p1);
            Assert.True(project1.Id == p1.Id);

            userService.CreateProject(user, ref project2);

            userService.SetActiveProject(user,ref  project1);
            //userService.SetActiveProject(user, ref project2);
        
            userService.GetActiveProject(user, out Project p);
            Assert.True(project1.Id == p.Id);
        }
        public void Dispose()
        {
            
            if(gxDb !=null)  gxDb.Dispose();
        }

        [Fact]
        public void SaveGxPropertyTest()
        {
            User user = userService.Login("Jack", "123");
            userService.GetActiveProject(user, out Project prj);
            Assert.NotNull(prj);
            
        }
    }
}
