using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> LocationList()
        {
            var values = await _locationService.GetAllLocationAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLocation(CreateLocationDto createLocationDto)
        {
            await _locationService.CreateLocationAsync(createLocationDto);
            return RedirectToAction("LocationList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return RedirectToAction("LocationList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            var value = await _locationService.GetLocationByIdAsync(id);
            var model = _mapper.Map<UpdateLocationDto>(value);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto updateLocationDto)
        {
            await _locationService.UpdateLocationAsync(updateLocationDto);
            return RedirectToAction("LocationList");
        }
    }
}
