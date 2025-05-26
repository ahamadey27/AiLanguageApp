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

        public void OnGet()
        {
            // Example: Set an initial message when the page loads
            Message = "Enter the 'sound code' in the format expected and click 'Decipher Sound Code'.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "There was an error with your submission.";
                return Page();
            }

            DecipheredTextDisplay = $"Deciphered text for '{DeciphererInputSoundCode}' will appear here.";
            Message = "Deciphered sound code. (Actual deciphering not yet implemented).";

            return Page();
        }
    }
}
