using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        LocationManager locationManager = new LocationManager(new EfLocationdal());
        HotelManager hotelManager = new HotelManager(new EfHotelDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var locations = locationManager.TLocationListAscName().ToPagedList(page, 10);
            return View(locations);
        }

        [HttpGet]
        public IActionResult Add() { return View(); }

        [HttpPost]
        public IActionResult Add(Location location)
        {
            LocationValidator validator = new LocationValidator();
            ValidationResult results = validator.Validate(location);

            if (results.IsValid)
            {
                locationManager.TInsert(location);
                TempData["SuccessMessage"] = "Location Was Added Successfully!";
                return RedirectToAction("Location", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(location);
        }

        public IActionResult Delete(int id)
        {
            var location = locationManager.TGetById(id);
            locationManager.TDelete(location);
            TempData["SuccessMessage"] = "Location Was Deleted Succesfully!";
            return RedirectToAction("Location", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var location = locationManager.TGetById(id);
            return View(location);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            LocationValidator validator = new LocationValidator();
            ValidationResult results = validator.Validate(location);

            if (results.IsValid)
            {
                var values = locationManager.TGetById(location.Id);
                values.Name = location.Name;
                locationManager.TUpdate(values);
                TempData["SuccessMessage"] = "Location Was Updated Successfully!";
                return RedirectToAction("Location", "Admin");
            }

            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View(location);
        }

        public IActionResult Hotels(int id, int page = 1)
        {
            var hotels = hotelManager.THotelsByLocation(id).ToPagedList(page, 10);
            ViewBag.Location = locationManager.TGetById(id).Name;
            return View(hotels);
        }

        public IActionResult DeleteSelected(int[] selectedLocations)
        {
            foreach (var locationId in selectedLocations)
            {
                var location = locationManager.TGetById(locationId);
                locationManager.TDelete(location);
            }

            TempData["SuccessMessage"] = "Selected Locations Was Deleted Successfully!";
            return RedirectToAction("Location", "Admin");
        }
    }
}
