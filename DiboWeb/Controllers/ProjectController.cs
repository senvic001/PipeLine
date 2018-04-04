using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiboWeb.Models;
using DiboWeb.Service;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiboWeb.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private GxDbContext gxdb;
        private UserService UserService;
        private ProjectService ProjectService;

        public ProjectController(GxDbContext gxDbContext)
        {
            gxdb = gxDbContext;
            UserService = new UserService(gxDbContext);
            ProjectService = new ProjectService(gxDbContext);
        }
        // GET: api/<controller>
        //项目中所有用户
        [HttpGet("{id}/users")]
        public IActionResult GetUsers(int id)
        {
            var prj = gxdb.Projects
                .Include(u=>u.UserProjects)
                    .ThenInclude(up => up.User)
                .SingleOrDefault(u => u.Id == id);
            if (prj == null) return NotFound();

            var users = new List<User>();
            foreach (var item in prj.UserProjects)
            {
                users.Add(item.User);
            }
            return new  ObjectResult(users);
        }

        //项目中查找用户
        [HttpGet("{id}/user/{userName}")]
        public IActionResult GetUserByName(int id, string userName)
        {
            Project prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return NotFound();

            var users = gxdb.Users.Where(u => u.Name == userName).ToList();
            return new ObjectResult(users);
        }
        //项目增加用户
        [HttpPost("{id}/user")]
        public IActionResult AddUser(int id, [FromBody] User user)
        {
            Project project = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (project == null) return NotFound();
            project.UserProjects.Add(new UserProject() { Project = project, User = user });
            gxdb.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}/user/{userId}")]
        public IActionResult DeleteUsers(int id,int userId)
        {
            Project prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return NotFound();

            User user = gxdb.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null) return NotFound();

            gxdb.Projects.Remove(prj);
            return new NoContentResult();
        }

        //*************数据操作 GxPoint*****************
        [HttpGet("{id}/gxpoints")]
        public IActionResult GetGxPoints(int id)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();

            return new ObjectResult( gxdb.GxPoints.Where(u => u.ProjectId == id).ToList());
        }
        [HttpPost("{id}/gxpoints")]
        public IActionResult AddGxPoints(int id,[FromBody] List<GxPoint> gxPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            var errorPoints = new List<GxPoint>();
            foreach (var p in gxPoints)
            {
                if (p.ProjectId == id)
                {
                    gxdb.GxPoints.Add(p);
                }
                else
                {
                     errorPoints.Add(p);
                }
            }
            gxdb.SaveChanges();
            if (errorPoints.Count == 0)
            {
                return Ok();
            }
            else
            {
                return new ObjectResult(errorPoints);
            }
        }

        [HttpPut("{id}/gxpoints")]
        public IActionResult UpdateGxPoints(int id ,[FromBody] List<GxPoint> gxPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GxPoints.UpdateRange(gxPoints);
            return Ok();
        }

        [HttpPut("{id}/gxpoints")]
        public IActionResult DeleteGxPoints(int id, [FromBody] List<GxPoint> gxPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GxPoints.RemoveRange(gxPoints);
            return Ok();
        }

        //*************数据操作 GxLine*****************
        [HttpGet("{id}/gxlines")]
        public IActionResult GetGxLines(int id)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();

            return new ObjectResult(gxdb.GxLines.Where(u => u.ProjectId == id).ToList());
        }
        [HttpPost("{id}/gxlines")]
        public IActionResult AddGxLines(int id, [FromBody] List<GxLine> gxLines)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            var errorLines = new List<GxLine>();
            foreach (var p in gxLines)
            {
                if (p.ProjectId == id)
                {
                    gxdb.GxLines.Add(p);
                }
                else
                {
                    errorLines.Add(p);
                }
            }
            gxdb.SaveChanges();
            if (errorLines.Count == 0)
            {
                return Ok();
            }
            else
            {
                return new ObjectResult(errorLines);
            }
        }

        [HttpPut("{id}/gxlines")]
        public IActionResult UpdateGxLines(int id, [FromBody] List<GxLine> gxLines)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GxLines.UpdateRange(gxLines);
            return Ok();
        }

        [HttpPut("{id}/gxlines")]
        public IActionResult DeleteGxLines(int id, [FromBody] List<GxLine> gxLines)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GxLines.RemoveRange(gxLines);
            return Ok();
        }

        //*************数据操作 GeoPoint*****************
        [HttpGet("{id}/geopoints")]
        public IActionResult GetGeoPoints(int id)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();

            return new ObjectResult(gxdb.GeoPoints.Where(u => u.ProjectId == id).ToList());
        }
        [HttpPost("{id}/geopoints")]
        public IActionResult AddGeoPoints(int id, [FromBody] List<GeoPoint> geoPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            var errorPoints = new List<GeoPoint>();
            foreach (var p in geoPoints)
            {
                if (p.ProjectId == id)
                {
                    gxdb.GeoPoints.Add(p);
                }
                else
                {
                    errorPoints.Add(p);
                }
            }
            gxdb.SaveChanges();
            if (errorPoints.Count == 0)
            {
                return Ok();
            }
            else
            {
                return new ObjectResult(errorPoints);
            }
        }

        [HttpPut("{id}/geopoints")]
        public IActionResult UpdateGeoPoints(int id, [FromBody] List<GeoPoint> geoPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GeoPoints.UpdateRange(geoPoints);
            return Ok();
        }

        [HttpPut("{id}/geopoints")]
        public IActionResult DeleteGeoPoints(int id, [FromBody] List<GeoPoint> geoPoints)
        {
            var prj = gxdb.Projects.SingleOrDefault(u => u.Id == id);
            if (prj == null) return BadRequest();
            gxdb.GeoPoints.RemoveRange(geoPoints);
            return Ok();
        }

    }
}
