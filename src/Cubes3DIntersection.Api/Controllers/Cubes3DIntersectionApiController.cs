using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cubes3DIntersection.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class Cubes3DIntersectionApiController : ControllerBase
    {

        private readonly ICube3DIntersectionService _cube3DIntersectionService;
        private readonly IAppLogger<Cubes3DIntersectionApiController> _logger;

        public Cubes3DIntersectionApiController(ICube3DIntersectionService cube3DIntersectionService,
            IAppLogger<Cubes3DIntersectionApiController> logger)
        {
            _cube3DIntersectionService = cube3DIntersectionService ?? throw new ArgumentNullException(nameof(cube3DIntersectionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("api/Cubes3DIntersection/post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cube3DIntersectionModel>> PostCubes3DIntersection([FromBody] Cube3DIntersectionModel cube3DIntersectionModel)
        {
            ValidatePostCubes3DIntersection(cube3DIntersectionModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelErrors());
            }

            try
            {
                var cubes3DDto = await _cube3DIntersectionService.Create(cube3DIntersectionModel);
                _logger.LogInformation($"Cubes 3D Intersection successfully generated.");
                return CreatedAtAction(nameof(PostCubes3DIntersection), new { id = cubes3DDto.Id }, cubes3DDto);
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when posting the Cubes 3D Intersection.", ex.Message);
                throw;
            }
        }

        private void ValidatePostCubes3DIntersection(Cube3DIntersectionModel cubes3DIntersectionModel)
        {
            if (cubes3DIntersectionModel == null)
            {
                _logger.LogError($"The List of Cubes cannot be null", nameof(cubes3DIntersectionModel));
                throw new ArgumentNullException(nameof(cubes3DIntersectionModel));
            }
        }

        private string ModelErrors()
        {
            var errorMessage = new StringBuilder();
            foreach (var error in ModelState.Values.SelectMany(value => value.Errors))
            {
                errorMessage.Append(error.Exception != null ? error.Exception.Message : error.ErrorMessage);
                errorMessage.Append(Environment.NewLine);
            }

            return errorMessage.ToString();
        }
    }
}