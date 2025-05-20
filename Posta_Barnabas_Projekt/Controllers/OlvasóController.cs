using Microsoft.AspNetCore.Mvc;
using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Services;

[ApiController]
[Route("api/[controller]")]
public class OlvasóController : ControllerBase
{
    private readonly IOlvasóService _olvasóService;

    public OlvasóController(IOlvasóService olvasóService)
    {
        _olvasóService = olvasóService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var olvasók = await _olvasóService.ListázásAsync();
        return Ok(olvasók);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var olvasó = await _olvasóService.LekérésAsync(id);
        if (olvasó == null) return NotFound();
        return Ok(olvasó);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Olvasó olvasó)
    {
        var létrehozott = await _olvasóService.LétrehozásAsync(olvasó);
        return CreatedAtAction(nameof(Get), new { id = létrehozott.Név }, létrehozott);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Olvasó olvasó)
    {
        var módosított = await _olvasóService.MódosításAsync(id, olvasó);
        if (módosított == null) return NotFound();
        return Ok(módosított);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var siker = await _olvasóService.TörlésAsync(id);
        if (!siker) return NotFound();
        return NoContent();
    }
}
