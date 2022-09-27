using Domain.Dto.Api;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Api.Controllers;

/// <summary>
/// Api controller to search postCode address 
/// </summary>
[ApiController]
[Route("api/postCode")]
public class PostcodeAddressController : ControllerBase
{
    private readonly ILogger<PostcodeAddressController> _logger;
    private readonly IUseCase<PostCodeLocationDto, PostCodeLocationCommandDto> _postCodeLocationUseCase;
    private readonly IUseCase<PostCodeLocationViewModelDto> _postCodeViewModelUseCase;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="postCodeLocationUseCase">Post code location use case</param>
    /// <param name="postCodeViewModelUseCase">Post code view model use case</param>
    public PostcodeAddressController(ILogger<PostcodeAddressController> logger, 
        IUseCase<PostCodeLocationDto, PostCodeLocationCommandDto> postCodeLocationUseCase,
        IUseCase<PostCodeLocationViewModelDto> postCodeViewModelUseCase)
    {
        _logger = logger;
        _postCodeLocationUseCase = postCodeLocationUseCase;
        _postCodeViewModelUseCase = postCodeViewModelUseCase;
    }

    /// <summary>
    /// Get location based on postCode
    /// </summary>
    /// <returns>Location dto</returns>
    [HttpGet("{postCode}")]
    [ProducesResponseType(200, Type = typeof(PostCodeLocationDto))]
    public async Task<PostCodeLocationDto> Get(string postCode)
    {
        var result = await _postCodeLocationUseCase.Execute(new PostCodeLocationCommandDto(postCode));

        return result;
    }

    /// <summary>
    /// Get location based on postCode
    /// </summary>
    /// <returns>Location dto</returns>
    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(PostCodeLocationViewModelDto))]
    public async Task<PostCodeLocationViewModelDto> Get()
    {
        var result = await _postCodeViewModelUseCase.Execute();

        return result;
    }
}
