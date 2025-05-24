using Microsoft.AspNetCore.Mvc;
using Posta_Barnabas_Projekt.Modellek;
using Posta_Barnabas_Projekt.Services;

[ApiController]
[Route("api/[controller]")]
public class KönyvController : ControllerBase
{
    private readonly IKönyvService _könyvService;

    public KönyvController(IKönyvService könyvService)
    {
        _könyvService = könyvService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var könyvek = await _könyvService.ListázásAsync();
        return Ok(könyvek);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var könyv = await _könyvService.LekérésAsync(id);
        if (könyv == null) return NotFound();
        return Ok(könyv);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Könyv könyv)
    {
        var létrehozott = await _könyvService.LétrehozásAsync(könyv);
        return CreatedAtAction(nameof(Get), new { id = létrehozott.Id }, létrehozott);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Könyv könyv)
    {
        var módosított = await _könyvService.MódosításAsync(id, könyv);
        if (módosított == null) return NotFound();
        return Ok(módosított);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var siker = await _könyvService.TörlésAsync(id);
        if (!siker) return NotFound();
        return NoContent();
    }
}
