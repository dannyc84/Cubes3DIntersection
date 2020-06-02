using Cubes3DIntersection.Application.Mappers;
using Cubes3DIntersection.Application.Models;
using Cubes3DIntersection.Core.Entities;
using Cubes3DIntersection.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Core.Interfaces;

namespace Cubes3DIntersection.Application.Services
{
    public class EdgeService : IEdgeService
    {
        private readonly IGenericRepository<Edge> _edgeRepository;
        private readonly IAppLogger<EdgeService> _logger;

        private const string serviceName = nameof(Point3DService);
        private const string withIdEqualTo = "with Id =";

        public EdgeService(IGenericRepository<Edge> repo, IAppLogger<EdgeService> logger)
        {
            _edgeRepository = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<EdgeModel>> GetCubes3DList()
        {
            var edgesList = await _edgeRepository.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<EdgeModel>>(edgesList);
            return mapped.ToList();
        }

        public async Task<EdgeModel> GetEdgeById(int edgeId)
        {
            var edge = await _edgeRepository.GetById(edgeId);
            var mapped = ObjectMapper.Mapper.Map<EdgeModel>(edge);
            return mapped;
        }

        public async Task<EdgeModel> Create(EdgeModel edge)
        {
            await ValidateIfExist(edge);

            var mappedEntity = ObjectMapper.Mapper.Map<Edge>(edge);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _edgeRepository.Create(mappedEntity);
            _logger.LogInformation($"Entity successfully added - { serviceName }");

            var newMappedEntity = ObjectMapper.Mapper.Map<EdgeModel>(newEntity);
            return newMappedEntity;
        }

        private async Task ValidateIfExist(EdgeModel edge)
        {
            if (edge == null) throw new ArgumentNullException(nameof(edge));

            var existingEntity = await _edgeRepository.GetById(edge.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{edge} with this id already exists");
        }

        public async Task Update(EdgeModel edge)
        {
            var mappedEntity = ObjectMapper.Mapper.Map<Edge>(edge);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            await _edgeRepository.Update(mappedEntity);
            _logger.LogInformation($"Entity successfully updated - {serviceName}");
        }
    }
}