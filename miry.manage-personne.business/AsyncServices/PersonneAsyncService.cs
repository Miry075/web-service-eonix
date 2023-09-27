using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using miry.async_service.client.AsyncServices;
using miry.manage_personne.business.Dtos.AsyncDtos;
using miry.manage_personne.business.Dtos.PersonneDtos;
using RabbitMQ.Client;

namespace miry.manage_personne.business.AsyncServices
{
	public class PersonneAsyncService: AsyncService<PersonneAsyncDto>, IPersonneAsyncService
    {
        private readonly ILogger<PersonneAsyncService> _logger;
        private readonly IMapper _mapper;

        public PersonneAsyncService(IConfiguration configuration, ILogger<PersonneAsyncService> logger, IMapper mapper) : base (configuration["RabbitMQHost"], configuration["RabbitMQPort"], configuration["RabbitMQExchange"])
        {
            logger.LogInformation("Create RabbitMQ connection...");
            CreateExchange(ExchangeType.Fanout);
            _logger = logger;
            _mapper = mapper;
        }

        public void PublishData(PersonneReadDto readDto, string eventName)
        {
            PersonneAsyncDto dataToPublish = _mapper.Map<PersonneAsyncDto>(readDto);
            dataToPublish.Event = eventName;
            _logger.LogInformation("Publlishing new platform...");
            PublishData(dataToPublish);
            _logger.LogInformation($"New personne {dataToPublish} has been published successfully!");
        }
    }
}

