using Microsoft.AspNetCore.Mvc;
using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Services;

[ApiController]
[Route("api/[controller]")]
public class KölcsönzésController : ControllerBase
{
    private readonly IKölcsönzésService _kölcsönzésService;

    public KölcsönzésController(IKölcsönzésService kölcsönzésService)
    {
        _kölcsönzésService = kölcsönzésService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _kölcsönzésService.ListázásAsync();
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Kölcsönzés kolcsonzes)
    {
        var új = await _kölcsönzésService.LétrehozásAsync(kolcsonzes);
        return CreatedAtAction(nameof(GetAll), new { id = új.Id }, új);
    }

    [HttpGet("olvasó/{olvasóId}")]
    public async Task<IActionResult> GetByOlvasó(int olvasóId)
    {
        var kolcsonzesek = await _kölcsönzésService.LekérésOlvasóSzerintAsync(olvasóId);
        return Ok(kolcsonzesek);
    }
}
