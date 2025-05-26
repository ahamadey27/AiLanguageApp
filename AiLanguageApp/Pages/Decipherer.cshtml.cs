using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AiLanguageApp.Pages
{
    public class DeciphererModel : PageModel
    {
        private readonly ILogger<DeciphererModel> _logger;

        //Property to bind the sound code input from another source
        [BindProperty]
        public string? DeciphererInputSoundCode { get; set; } //string? (nullable string) indicates properties might be null

        //Property to hold the deciphered text to display on the page
        [BindProperty(SupportsGet = true)]
        public string? DecipheredTextDisplay { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; } // Optional message property

        public DeciphererModel(ILogger<DeciphererModel> logger)
        {
            _logger = logger;
        }

        public class DeciphererModel : PageModel
        {
            public void OnGet()
            {
                // Example: Set an initial message when the page loads
                Message = "Enter the 'sound code' in the format expected and click 'Decipher Sound Code'.";
            }

        }

        

        // Placeholder for future POST handler
        // public async Task<IActionResult> OnPostAsync()
        // {
        //     // Logic for deciphering code will go here
        //     return Page();
        // }
    }
}
