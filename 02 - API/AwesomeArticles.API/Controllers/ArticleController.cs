using AwesomeArticles.API.ApiResult;
using AwesomeArticles.Application.Services.Implementations;
using AwesomeArticles.Application.Services.Interfaces;
using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeArticles.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("/api/articles/all")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleService.GetArticles();

                return Ok(new Result("Artigos obtidos", true, articles));
            }
            catch (DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }

        [HttpGet("/api/articles/{idArticle}")]
        public async Task<IActionResult> GetArticle(Guid idArticle)
        {
            try
            {
                var article = await _articleService.GetArticle(idArticle);

                return Ok(new Result("Artigo obtido", true, article));
            }
            catch (DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }

        [HttpPost("/api/articles/add")]
        public async Task<IActionResult> AddArticle([FromBody] ArticleViewModel articleViewModel)
        {
            try
            {
                var article = await _articleService.AddArticle(articleViewModel);

                return Ok(new Result("Artigo criado!", true, article));
            }
            catch (DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }

        [HttpPost("/api/articles/{idArticle}/add-like")]
        public async Task<IActionResult> AddArticleLike(Guid idArticle)
        {
            try
            {
                var article = await _articleService.AddLike(idArticle);

                return Ok(new Result("Like dado!", true, article));
            }
            catch (DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }

        [HttpDelete("/api/articles/{idArticle}/remove-like")]
        public async Task<IActionResult> RemoveArticleLike(Guid idArticle)
        {
            try
            {
                var article = await _articleService.RemoveLike(idArticle);

                return Ok(new Result("Like removido!", true, article));
            }
            catch (DomainException ex)
            {
                return StatusCode(400, Result.ResultBuilder.DomainError(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Result.ResultBuilder.InternalServerError());
            }
        }
    }
}
