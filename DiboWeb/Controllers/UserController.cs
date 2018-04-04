using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiboWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiboWeb.Service;

namespace DiboWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private GxDbContext gxdb;
        private UserService UserService;
        public UserController(GxDbContext gxDbContext)
        {
            gxdb = gxDbContext;
            UserService = new UserService(gxDbContext);
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            List<User> users = gxdb.Users
                .Include(u => u.CreatedProjects)
                .ToList();
            return users;
        }
        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                User user = gxdb.Users
                                .Single(u => u.Id == id);
                return new ObjectResult(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound();;
            }
        }
        // GET: api/User/5
        [HttpGet("{id}/createdprojects")]
        public IActionResult GetCreatedProject(int id)
        {
            try
            {
                User user = gxdb.Users
                    .Include(u =>u.CreatedProjects)
                                .SingleOrDefault(u => u.Id == id);
                if (user == null) return NotFound();
                if (user.CreatedProjects.Count == 0) return NotFound();


                return new ObjectResult(user.CreatedProjects);
            }
            catch (InvalidOperationException)
            {
                return NotFound(); ;
            }
        }
        // GET: api/User/1/projects
        [HttpGet("{id}/projects")]
        public IActionResult GetProject(int id)
        {
            try
            {
                User user = gxdb.Users
                    .Include(u => u.UserProjects)
                               .ThenInclude(up => up.Project)
                               .SingleOrDefault(u => u.Id == id);
                if (user == null) return NotFound();
                if (user.UserProjects.Count == 0) return NotFound();

                var prjs = new List<Project>(user.UserProjects.Count);
                foreach (var item in user.UserProjects)
                {
                    prjs.Add(item.Project);
                }

                return new ObjectResult(prjs);
            }
            catch (InvalidOperationException)
            {
                return NotFound(); ;
            }
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null) return BadRequest();
            if (!UserService.Register(ref user)) return BadRequest();

            return CreatedAtRoute("Get", new { id = user.Id }, user);
        }

        [HttpPost("{id}/project")]
        public IActionResult CreateProject(int id ,[FromBody] Project  project )
        {
            if (project == null) return BadRequest();
            User user = gxdb.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            //Todo 返回错误对象
            if(!UserService.CreateProject(user, ref project) ) return new ObjectResult(project) ;

            return new ObjectResult(project);
        }

        [HttpPut("{id}/project")]
        public IActionResult UpdateProject(int id, [FromBody] Project project)
        {
            if (project == null) return BadRequest();
            Project prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null || prj.Id!=project.Id) return NotFound();

            gxdb.Projects.Update(project);

            return new NoContentResult();
        }

        [HttpDelete("{id}/project/{projectId}")]
        public IActionResult DeleteProject(int id ,int projectId)
        {
            User user = gxdb.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            Project prj = gxdb.Projects.SingleOrDefault(u => u.Id == projectId);
            if (prj == null) return NotFound();

            gxdb.Projects.Remove(prj);

            return new NoContentResult();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }
            var user1 = gxdb.Users.SingleOrDefault(u => u.Id == id);
            if (user1 == null) return NotFound();

            user1.Name = user.Name;
            user1.PassWord = user.PassWord;
            gxdb.SaveChanges();

            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = gxdb.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            gxdb.Users.Remove(user);
            gxdb.SaveChanges();

            return new NoContentResult();
        }
    }
}
