﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharma360WebApi.Authorization;
using Pharma360WebApi.Models.DOTs;
using Pharma360WebApi.Services.Categoria;

namespace Pharma360WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [HasPermission("perm.category.read")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [HasPermission("perm.category.read")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        [HasPermission("perm.category.create")]
        public async Task<ActionResult<CategoriaDto>> Create(CategoriaDto dto)
        {
            var created = await _categoriaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.IdCategoria }, created);
        }

        [HttpPut("{id}")]
        [HasPermission("perm.category.update")]
        public async Task<IActionResult> Update(int id, CategoriaDto dto)
        {
            var updated = await _categoriaService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [HasPermission("perm.category.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoriaService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
