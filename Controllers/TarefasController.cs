using Microsoft.AspNetCore.Mvc;
using vSaude.Models;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vSaude.Controllers
{
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<TarefasController> _logger;

        public TarefasController(IUnitOfWork uow, IMapper mapper, ILogger<TarefasController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<Tarefas>
        [HttpGet("api/tarefas")]
        public async Task<ActionResult<IEnumerable<TarefaViewDto>>> Get([FromQuery] StatusEnum? status = null, [FromQuery] PrioridadeEnum? prioridade = null, [FromQuery] CategoriaEnum? categoria = null, [FromQuery] string? busca = null, [FromQuery] DateTime? dataInicio = null, [FromQuery] DateTime? dataFim = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // TODO: Implementar cache com Redis nesses endpoints
            // - Verificar se existe cache válido antes de consultar o banco
            // - Se existir cache, retornar dados cacheados
            // - Se não existir, buscar do banco e salvar no cache por alguns minutos
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = _uow.Tarefas.Query();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (prioridade.HasValue)
            {
                query = query.Where(t => t.Prioridade == prioridade.Value);
            }

            if (categoria.HasValue)
            {
                query = query.Where(t => t.Categoria == categoria.Value);
            }

            if (!string.IsNullOrWhiteSpace(busca))
            {
                var term = busca.Trim();
                query = query.Where(t => t.Titulo.Contains(term) || (t.Descricao ?? string.Empty).Contains(term));
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(t => t.DataCriacao >= dataInicio.Value.Date);
            }
            if (dataFim.HasValue)
            {
                var fim = dataFim.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(t => t.DataCriacao <= fim);
            }

            var total = await query.CountAsync();
            var itens = await query
                .OrderByDescending(t => t.DataCriacao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (HttpContext != null)
            {
                Response.Headers["X-Total-Count"] = total.ToString();
                Response.Headers["X-Page"] = page.ToString();
                Response.Headers["X-Page-Size"] = pageSize.ToString();
            }

            var dto = _mapper.Map<IEnumerable<TarefaViewDto>>(itens);
            return Ok(new
            {
                mensagem = $"{total} tarefa(s) encontrada(s)",
                total = total,
                pagina = page,
                tamanhoPagina = pageSize,
                tarefas = dto
            });
        }

        // GET api/<Tarefas>/5
        [HttpGet("api/tarefas/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var tarefa = await _uow.Tarefas.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada" });
            }
            var dto = _mapper.Map<TarefaViewDto>(tarefa);
            return Ok(new
            {
                mensagem = "Tarefa encontrada",
                tarefa = dto
            });
        }

        // POST api/<Tarefas>
        [HttpPost("api/tarefas")]
        public async Task<ActionResult> Post([FromBody] TarefaCreateDto value)
        {
            var entity = _mapper.Map<TarefaMedica>(value);
            await _uow.Tarefas.AddAsync(entity);
            await _uow.SaveChangesAsync();
            var dto = _mapper.Map<TarefaViewDto>(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, new
            {
                mensagem = "Tarefa criada com sucesso",
                tarefa = dto
            });
        }

        // PUT api/<Tarefas>/5
        [HttpPut("api/tarefas/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TarefaUpdateDto tarefa)
        {
            var entity = await _uow.Tarefas.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada" });
            }
            _mapper.Map(tarefa, entity);
            _uow.Tarefas.Update(entity);
            await _uow.SaveChangesAsync();
            var dto = _mapper.Map<TarefaViewDto>(entity);
            return Ok(new
            {
                mensagem = "Tarefa atualizada com sucesso",
                tarefa = dto
            });
        }

        [HttpPatch("api/tarefas/{id}/status")]
        public async Task<ActionResult> PatchStatus(int id, [FromBody] StatusEnum status)
        {
            var tarefa = await _uow.Tarefas.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada" });
            }
            tarefa.Status = status;
            _uow.Tarefas.Update(tarefa);
            await _uow.SaveChangesAsync();
            var dto = _mapper.Map<TarefaViewDto>(tarefa);
            return Ok(new
            {
                mensagem = "Status atualizado com sucesso",
                tarefa = dto
            });
        }

        // DELETE api/<Tarefas>/5
        [HttpDelete("api/tarefas/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _uow.Tarefas.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound(new { mensagem = "Tarefa não encontrada" });
            }
            _uow.Tarefas.SoftDelete(tarefa);
            await _uow.SaveChangesAsync();
            return Ok(new { mensagem = "Tarefa deletada com sucesso", id = id });
        }
    }
}
