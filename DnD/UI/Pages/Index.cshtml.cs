using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UI.Models;

namespace UI.Pages;

public class IndexModel : PageModel
{
    [BindProperty] 
    public Character? Player { get; set; }
    
    public Character? Monster { get; set; }
    
    public FightResult? FightResult { get; set; }

    private readonly HttpClient _client = new();

    private readonly Uri _dbUrl =
        new("http://localhost:5109/Monster/Random");

    private readonly Uri _serverUrl =
        new("http://localhost:5100/Fight/Calculate");

    public void OnGet()
    {
    }

    public async Task OnPost()
    {
        if (!ModelState.IsValid) return;
        
        Monster = await _client.GetFromJsonAsync<Character>(_dbUrl)
            ?? throw new Exception("Cant get monster");

        var fightingCharacters = new FightingCharacters
        {
            Player = Player!,
            Monster = Monster
        };

        var response = await _client.PostAsJsonAsync(_serverUrl, fightingCharacters);
        FightResult = await response.Content.ReadFromJsonAsync<FightResult>();
    }
}