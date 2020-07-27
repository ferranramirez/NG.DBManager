﻿using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using System;
using System.Threading.Tasks;

namespace NG.DBManager.Presentation.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class AudioImageController : ControllerBase
    {
        private readonly IFullUnitOfWork _uow;
        private readonly NgContext _context;

        public AudioImageController(IFullUnitOfWork uow, NgContext context)
        {
            _uow = uow;
            _context = context;
        }

        /// <summary>
        /// Get AudioImage
        /// </summary>
        [HttpGet("{AudioImageId}")]
        public IActionResult Get(Guid AudioId, Guid ImageId)
        {
            var AudioImage = _uow.Repository<CommerceDeal>().Find(ai => ai.AudioId == AudioId && ai.ImageId == ImageId);
            return Ok(AudioImage);
        }

        /// <summary>
        /// Get All AudioImages
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _uow.Repository<CommerceDeal>().GetAll());
        }

        /// <summary>
        /// Add AudioImage
        /// </summary>
        [HttpPost()]
        public IActionResult Add(CommerceDeal AudioImage)
        {
            _uow.Repository<CommerceDeal>().Add(AudioImage);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Update AudioImage
        /// </summary>
        [HttpPut]
        public IActionResult Update(CommerceDeal AudioImage)
        {
            _uow.Repository<CommerceDeal>().Update(AudioImage);
            return Ok(_uow.Commit());
        }

        /// <summary>
        /// Remove AudioImage
        /// </summary>
        [HttpDelete("{AudioImageId}")]
        public IActionResult Remove(Guid AudioId, Guid ImageId)
        {
            var AudioImage = _uow.Repository<CommerceDeal>().Find(ai => ai.AudioId == AudioId && ai.ImageId == ImageId);
            _uow.Repository<CommerceDeal>().Remove(AudioImage);
            return Ok(_uow.Commit());
        }
    }
}
