using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.AppServices.Interfaces;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.Web.Api.Models.Entities;
using MoneyTrack.Web.Api.Models.Entities.Operational;

namespace MoneyTrack.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionController : MoneyTrackController
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getLast")]
        public async Task<IActionResult> GetLastTransactions([FromQuery]int currentPage, [FromQuery]int pageSize)
        {
            var paging = new Paging
            {
                CurrentPage = currentPage,
                PageSize = pageSize
            };

            var transactions = await _transactionService.GetLastTransactions(paging);

            return Ok(_mapper.Map<List<TransactionModel>>(transactions));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]TransactionModel transactionModel)
        {
            var transactionDto = _mapper.Map<TransactionDto>(transactionModel);

            transactionDto.User = new UserDto { Id = GetCurrentUserId() };

            await _transactionService.Add(transactionDto);

            return Ok();
        }

        [HttpPost]
        [Route("getFiltered")]
        public async Task<IActionResult> GetWithFilters([FromBody] DbQueryModel request)
        {
            var dbRequest = _mapper.Map<DbQueryRequest>(request);

            var transactions = await _transactionService.GetQueryTransactions(dbRequest);

            return Ok(_mapper.Map<List<TransactionModel>>(transactions));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _transactionService.Delete(id);

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody]TransactionModel transactionModel)
        {
            var dto = _mapper.Map<TransactionDto>(transactionModel);

            await _transactionService.Update(dto);

            return Ok();
        }
    }
}
