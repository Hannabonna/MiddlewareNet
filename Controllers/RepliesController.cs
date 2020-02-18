using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiddlewareNet.Controllers
{
    [ApiController]
    [Route("replies")]
    public class RepliesController : ControllerBase
    {
        private static List<IdReplies> Reply = new List<IdReplies>()
        {
            new IdReplies(){Id=1, Content="qwerty", MemberId= 2, TopicId= 2},
            new IdReplies(){Id=2, Content="qwerty", MemberId= 2, TopicId= 2},
            new IdReplies(){Id=3, Content="qwerty", MemberId= 2, TopicId= 2},
            new IdReplies(){Id=4, Content="qwerty", MemberId= 2, TopicId= 2},
            new IdReplies(){Id=5, Content="qwerty", MemberId= 2, TopicId= 2}
        };

        private readonly ILogger<RepliesController> _logger;

        public RepliesController(ILogger<RepliesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status="success", message="success get Data", data = Reply});
        }

        [HttpPost]
        public IActionResult RepliesAdd(IdReplies rip)
        {
            var addRep = new IdReplies(){Id=rip.Id, Content = rip.Content, MemberId = rip.MemberId, TopicId = rip.TopicId};
            Reply.Add(addRep);
            return Ok(new { status = "success", message = "success add Data", data = Reply});
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(Reply.Find( e => e.Id == id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReplies(int id)
        {
            var del = Reply.Find( e => e.Id == id);
            Reply.Remove(del);
            return Ok(new { status = "deleted", message = "success delete some data", data = Reply});
        }

        [HttpPatch("{id}")]
        public IActionResult PatchReplies(int id, [FromBody] JsonPatchDocument<IdReplies> patchRep)
        {
            patchRep.ApplyTo(Reply.Find(e => e.Id == id));
            return Ok( new { status = "updated", message = "success update data", data = Reply.Find(e => e.Id == id) });
        }
        
    }
}
