using DapperMvcDemo.Data.Models.Domain;
using DapperMvcDemo.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperMvcDemo.Web.Controllers
{
    public class PersonController1 : Controller
    {
        private readonly IPersonRepository _personRepo;

        public PersonController1(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(person);
                }
                bool addPersonResult= await _personRepo.AddAsync(person);
                if(addPersonResult)
                {
                    TempData["msg"] = "Successfully added";
                }
                else
                {
                    TempData["msg"] = "Could not added";
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added";
            }
            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the person and pass it to the view if needed
            var person = await _personRepo.GetByIdAsync(id);
          //  if (person == null)
           // {
          //      throw NotFound();
         //   }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }

                var updateResult = await _personRepo.UpdateAsync(person);
                if (updateResult)
                {
                    TempData["msg"] = "Updated successfully";
                    return RedirectToAction(nameof(DisplayAll));  // Redirect to the list after updating
                }
                else
                {
                    TempData["msg"] = "Could not update";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not update";
            }

            return View(person);
        }


        public async Task<IActionResult> DisplayAll()
        {
            var people = await _personRepo.GetAllAsync();
            return View(people);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }
    }
}
