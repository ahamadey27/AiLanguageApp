using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AiLanguageApp.Pages
{
    public class GeneratorModel : PageModel
    {
        private readonly ILogger<GeneratorModel> _logger;

        // Property to bind the text input from the form
        [BindProperty]
        public string? GeneratorInputText { get; set; } // The name matches the 'name' attribute of your textarea

        // Property to hold the generated sound code to display on the page
        // SupportsGet = true allows this property to be populated and displayed
        // even on a GET request (e.g., after a POST-Redirect-GET pattern, or if set in OnGet)
        [BindProperty(SupportsGet = true)]
        public string? SoundCodeDisplay { get; set; }

        // You might also want a property for general messages to the user
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }

        public GeneratorModel(ILogger<GeneratorModel> logger)
        {
            _logger = logger;
        }

        public class GeneratorModel : PageModel
        {
            public void OnGet()
            {
                //Example: Set an initial message when the page loads
                Message = "Enter text below and click 'Generate & Play Sound'.";


            }

        }

        

        // We will implement the OnPostAsync handler in a later step
        // For now, just having the properties is the goal.
    }
}
