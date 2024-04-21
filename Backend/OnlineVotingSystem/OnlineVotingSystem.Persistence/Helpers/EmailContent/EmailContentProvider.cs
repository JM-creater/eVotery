using Microsoft.Extensions.Configuration;
using OnlineVotingSystem.Application.Email.Config;

namespace OnlineVotingSystem.Persistence.Helpers.EmailContent;

public class EmailContentProvider
{
    private readonly IConfiguration configuration;

    public EmailContentProvider(IConfiguration _configuration)
    {
        configuration = _configuration;
    }

    public async Task SendVoterIdInformation(string email, int voterid)
    {
        string subject = "Voter Registration Confirmation";
        string message = $@"<html>
                    <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            background-color: #ffffff;
                            width: 100%;
                            max-width: 600px;
                            margin: 20px auto;
                            padding: 20px;
                            box-shadow: 0 4px 8px rgba(0,0,0,0.05);
                        }}
                        .header {{
                            background-color: #4CAF50;
                            color: #ffffff;
                            padding: 10px 20px;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .footer {{
                            font-size: 12px;
                            text-align: center;
                            color: #888888;
                            padding: 20px;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 10px 20px;
                            margin-top: 20px;
                            background-color: #4CAF50;
                            color: #ffffff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .button a {{
                            text-decoration: none;
                        }}
                    </style>
                    </head>
                    <body>
                    <div class=""container"">
                        <div class=""header"">
                            <h1>Voter Registration Confirmed</h1>
                        </div>
                        <div class=""content"">
                            <p>Dear Voter,</p>
                            <p>Thank you for registering to vote. Your registration was successful!</p>
                            <p>Your Voter ID is: <strong>{voterid}</strong></p>
                            <p>Please keep this ID safe as it will be required for identification on voting day.</p>
                            <a href=""http://localhost:5173/"" class=""button"">Visit Our Website</a>
                        </div>
                        <div class=""footer"">
                            © 2024 eVotery. All rights reserved.
                        </div>
                    </div>
                    </body>
                    </html>";

        await EmailConfiguration.SendEmailAsync(email, subject, message, configuration);
    }

    public async Task SendPasswordResetEmail(string email, string token)
    {
        string resetLink = $"http://localhost:5173/forgot-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
        string subject = "Password Reset Request";
        string message = $@"
                <html>
                    <head>
                        <style>
                            body {{
                                font-family: 'Arial', sans-serif;
                                color: #333;
                                background-color: #f4f4f4;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                background: #fff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0,0,0,0.1);
                            }}
                            .button {{
                                display: inline-block;
                                padding: 10px 20px;
                                background-color: #007bff;
                                border-radius: 5px;
                                text-decoration: none;
                                font-weight: bold;
                            }}
                            .button a {{
                                color: #fff; 
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Password Reset Request</h2>
                            <p>You requested a password reset for your account. Please click the button below to set a new password:</p>
                            <a href='{resetLink}' class='button'>Reset Password</a>
                            <p>If you did not request a password reset, please ignore this email.</p>
                        </div>
                    </body>
                </html>";

        await EmailConfiguration.SendEmailAsync(email, subject, message, configuration);
    }
}
