using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riddle.Models;
using Riddle.ViewModels;

namespace Riddle.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRiddleRepository _repo;
       // private readonly IFileManager _fileManager;(also need to inject IFileManager)

        public PanelController(IRiddleRepository repo)
        {
            _repo = repo;
           // _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var model = _repo.GetAllRiddle();
            return View(model);
        }

        public IActionResult Post(int Id)
        {
            var model = _repo.GetRiddle(Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {

                return View(new RiddlePost());
            }
            else
            {
                var model = _repo.GetRiddle((int)Id);
                return View(model);
             
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RiddlePost riddlePost)
        {
            if (riddlePost.Id > 0)
            {
                _repo.Update(riddlePost);
            }
            else
            {
                _repo.Add(riddlePost);
            }

            if (await _repo.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(riddlePost);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            _repo.DeleteRiddle(Id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}