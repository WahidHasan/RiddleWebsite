using Microsoft.AspNetCore.Mvc;
using Riddle.Models;
using Riddle.Models.Comments;
using Riddle.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riddle.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRiddleRepository _repo;
       // private readonly IFileManager _fileManager;

        public HomeController(IRiddleRepository repo)
        {
            _repo = repo;
            // _fileManager = fileManager;
            
        }

        public IActionResult Index(string  riddleType) =>
            View(string.IsNullOrEmpty(riddleType) ? _repo.GetAllRiddle() : _repo.GetAllRiddle(riddleType));


        /*public IActionResult Index(string riddleType)
        {
            var model = string.IsNullOrEmpty(riddleType) ? _repo.GetAllRiddle() : _repo.GetAllRiddle(riddleType);
            //boolean ? true : false
            return View(model);
        }*/

        public IActionResult Post(int Id)=>
            View(_repo.GetRiddle(Id));
        

        /*public IActionResult Post(int Id)
        {
            var model = _repo.GetRiddle(Id);
            return View(model);
        }*/

        [HttpGet]
        public IActionResult Answer(int Id) =>
        
            //var model = _repo.GetRiddle(Id);
            //var xt = model.Answer;
            PartialView("_AnswerPartial", _repo.GetRiddle(Id));
            //return new JsonResult(xt);
        

        /*[HttpGet]
        public IActionResult Answer(int Id)
        {
            var model = _repo.GetRiddle(Id);
            var xt = model.Answer;
            return PartialView("_AnswerPartial", model);
            //return new JsonResult(xt);
        }*/
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction( "Post",new { Id = commentViewModel.PostId });

            var post = _repo.GetRiddle(commentViewModel.PostId);
            if(commentViewModel.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                    {
                    Message = commentViewModel.Message,
                    Created = DateTime.Now

                });
                _repo.Update(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = commentViewModel.MainCommentId,
                    Message = commentViewModel.Message,
                    Created = DateTime.Now
                };
                _repo.AddSubComment(comment);
            }
            await _repo.SaveChangesAsync();
            return RedirectToAction("Post", new { Id = commentViewModel.PostId });

        }

        /*[HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image),$"image/{mime}");
        }*/
    }
}