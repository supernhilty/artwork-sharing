﻿using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.Packages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtworkSharing.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ManagePackageController : Controller
	{
		private readonly IPackageService _packageService;

		public ManagePackageController(IPackageService packageService)
		{
			_packageService = packageService;
		}

		[HttpGet]
		public async Task<ActionResult<List<PackageViewModel>>> GetAllPackages()
		{
			var packages = await _packageService.GetAll();
			return Ok(packages);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<PackageViewModel>> GetPackage(Guid id)
		{
			var package = await _packageService.GetOne(id);
			if (package == null)
			{
				return NotFound();
			}
			return Ok(package);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePackage(Guid id, Package packageInput)
		{
			try
			{
				await _packageService.Update(packageInput);
				return Ok(packageInput);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreatePackage(Package packageInput)
		{
			try
			{
				await _packageService.Add(packageInput);
				return CreatedAtAction(nameof(GetPackage), new { id = packageInput.Id }, packageInput);
			}
			catch (Exception)
			{
				return StatusCode(500); // Internal Server Error
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePackage(Guid id)
		{
			try
			{
				await _packageService.Delete(id);
				return NoContent();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
