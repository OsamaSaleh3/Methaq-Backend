using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Emails
{

    public static class EmailTemplates
    {

        public static string OtpConfirmation() => "Confirm Your Methaq Account";
        public static string AccountApproved() => "Your Methaq Account Has Been Approved";
        public static string AccountRejected() => "Update on Your Methaq Account Registration";
        public static string EnrollmentApproved() => "Your Enrollment Request Has Been Approved";
        public static string EnrollmentRejected() => "Update on Your Enrollment Request";
        public static string FinalReport() => "Your Final Report is Ready";
        public static string ForgotPassword() => "Reset Your Methaq Password";

        public static string ForgotPassword(string fullName, string otp) => $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding: 40px 0;">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
                            <tr>
                                <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                    <h1 style="color: #ffffff; margin: 0; font-size: 24px;">Methaq</h1>
                                    <p style="color: #a8d5b5; margin: 5px 0 0;">Quran Memorization System</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 40px 30px;">
                                    <h2 style="color: #333333; margin: 0 0 10px;">Hello, {fullName}</h2>
                                    <p style="color: #666666; line-height: 1.6;">We received a request to reset your password. Use the code below to proceed.</p>
                                    <table width="100%" cellpadding="0" cellspacing="0" style="margin: 30px 0;">
                                        <tr>
                                            <td align="center">
                                                <div style="background-color: #f0f7f4; border: 2px dashed #3B666B; border-radius: 8px; padding: 20px 40px; display: inline-block;">
                                                    <p style="color: #666666; margin: 0 0 8px; font-size: 14px;">Your password reset code</p>
                                                    <p style="color: #3B666B; font-size: 36px; font-weight: bold; margin: 0; letter-spacing: 8px;">{otp}</p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style="color: #666666; line-height: 1.6;">This code will expire in <strong>10 minutes</strong>. If you did not request a password reset, please ignore this email.</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                    <p style="color: #999999; font-size: 12px; margin: 0;">This is an automated message, please do not reply.</p>
                                    <p style="color: #999999; font-size: 12px; margin: 5px 0 0;">© 2025 Methaq. All rights reserved.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>
        """;

        public static string OtpConfirmation(string fullName, string otp) => $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding: 40px 0;">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
                            <tr>
                                <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                    <h1 style="color: #ffffff; margin: 0; font-size: 24px;">Methaq</h1>
                                    <p style="color: #a8d5b5; margin: 5px 0 0;">Quran Memorization System</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 40px 30px;">
                                    <h2 style="color: #333333; margin: 0 0 10px;">Hello, {fullName}</h2>
                                    <p style="color: #666666; line-height: 1.6;">Thank you for registering. Please use the verification code below to confirm your email address.</p>
                                    <table width="100%" cellpadding="0" cellspacing="0" style="margin: 30px 0;">
                                        <tr>
                                            <td align="center">
                                                <div style="background-color: #f0f7f4; border: 2px dashed #3B666B; border-radius: 8px; padding: 20px 40px; display: inline-block;">
                                                    <p style="color: #666666; margin: 0 0 8px; font-size: 14px;">Your verification code</p>
                                                    <p style="color: #3B666B; font-size: 36px; font-weight: bold; margin: 0; letter-spacing: 8px;">{otp}</p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <p style="color: #666666; line-height: 1.6;">This code will expire in <strong>10 minutes</strong>. If you did not create an account, please ignore this email.</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                    <p style="color: #999999; font-size: 12px; margin: 0;">This is an automated message, please do not reply.</p>
                                    <p style="color: #999999; font-size: 12px; margin: 5px 0 0;">© 2025 Methaq. All rights reserved.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>
        """;

        public static string AccountApproved(string fullName) => $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding: 40px 0;">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
                            <tr>
                                <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                    <h1 style="color: #ffffff; margin: 0; font-size: 24px;">Methaq</h1>
                                    <p style="color: #a8d5b5; margin: 5px 0 0;">Quran Memorization System</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 40px 30px;">
                                    <h2 style="color: #333333; margin: 0 0 10px;">Hello, {fullName}</h2>
                                    <div style="background-color: #f0f7f4; border-left: 4px solid #3B666B; padding: 15px 20px; margin: 20px 0; border-radius: 4px;">
                                        <p style="color: #3B666B; font-weight: bold; margin: 0;">✓ Your account has been approved!</p>
                                    </div>
                                    <p style="color: #666666; line-height: 1.6;">You can now log in to Methaq and start using the system.</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                    <p style="color: #999999; font-size: 12px; margin: 0;">This is an automated message, please do not reply.</p>
                                    <p style="color: #999999; font-size: 12px; margin: 5px 0 0;">© 2025 Methaq. All rights reserved.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>
        """;


        public static string AccountRejected(string fullName, string? reason) => $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" style="padding: 40px 0;">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
                            <tr>
                                <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                    <h1 style="color: #ffffff; margin: 0; font-size: 24px;">Methaq</h1>
                                    <p style="color: #a8d5b5; margin: 5px 0 0;">Quran Memorization System</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 40px 30px;">
                                    <h2 style="color: #333333; margin: 0 0 10px;">Hello, {fullName}</h2>
                                    <div style="background-color: #fff5f5; border-left: 4px solid #e53e3e; padding: 15px 20px; margin: 20px 0; border-radius: 4px;">
                                        <p style="color: #e53e3e; font-weight: bold; margin: 0;">✗ Your account registration was not approved.</p>
                                    </div>
                                    {(reason != null ? $"""
                                    <div style="background-color: #f9f9f9; padding: 15px 20px; border-radius: 4px; margin: 10px 0;">
                                        <p style="color: #666666; margin: 0;"><strong>Reason:</strong> {reason}</p>
                                    </div>
                                    """ : "")}
                                    <p style="color: #666666; line-height: 1.6;">If you believe this is a mistake, please contact the administrator.</p>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                    <p style="color: #999999; font-size: 12px; margin: 0;">This is an automated message, please do not reply.</p>
                                    <p style="color: #999999; font-size: 12px; margin: 5px 0 0;">© 2025 Methaq. All rights reserved.</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>
        """;

        public static string EnrollmentApproved(string fullName, string centerName) => $"""
    <!DOCTYPE html>
    <html lang="en">
    <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="padding: 40px 0;">
                    <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden;">
                        <tr>
                            <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                <h1 style="color: #ffffff; margin: 0;">Methaq</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 40px 30px;">
                                <h2 style="color: #333333;">Hello, {fullName}</h2>
                                <div style="background-color: #f0f7f4; border-left: 4px solid #3B666B; padding: 15px 20px; margin: 20px 0;">
                                    <p style="color: #3B666B; font-weight: bold; margin: 0;">✓ Your enrollment request has been approved!</p>
                                </div>
                                <p style="color: #666666;">You have been successfully enrolled in <strong>{centerName}</strong>.</p>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                <p style="color: #999999; font-size: 12px; margin: 0;">© 2025 Methaq. All rights reserved.</p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
    </html>
    """;

        public static string EnrollmentRejected(string fullName, string centerName, string? reason) => $"""
    <!DOCTYPE html>
    <html lang="en">
    <body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="padding: 40px 0;">
                    <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden;">
                        <tr>
                            <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                                <h1 style="color: #ffffff; margin: 0;">Methaq</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 40px 30px;">
                                <h2 style="color: #333333;">Hello, {fullName}</h2>
                                <div style="background-color: #fff5f5; border-left: 4px solid #e53e3e; padding: 15px 20px; margin: 20px 0;">
                                    <p style="color: #e53e3e; font-weight: bold; margin: 0;">✗ Your enrollment request for {centerName} was not approved.</p>
                                </div>
                                {(reason != null ? $"<p style='color: #666666;'><strong>Reason:</strong> {reason}</p>" : "")}
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                                <p style="color: #999999; font-size: 12px; margin: 0;">© 2025 Methaq. All rights reserved.</p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
    </html>
    """;

        public static string FinalReport(string fullName, decimal memorizationScore, decimal attendanceScore, decimal participationScore, decimal behaviorScore, decimal totalScore) => $"""
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body style="margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" style="padding: 40px 0;">
                <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1);">
                    <tr>
                        <td style="background-color: #3B666B; padding: 30px; text-align: center;">
                            <h1 style="color: #ffffff; margin: 0; font-size: 24px;">Methaq</h1>
                            <p style="color: #a8d5b5; margin: 5px 0 0;">Quran Memorization System</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 40px 30px;">
                            <h2 style="color: #333333; margin: 0 0 10px;">Hello, {fullName}</h2>
                            <div style="background-color: #f0f7f4; border-left: 4px solid #3B666B; padding: 15px 20px; margin: 20px 0; border-radius: 4px;">
                                <p style="color: #3B666B; font-weight: bold; margin: 0;">✓ Your final report is now available.</p>
                            </div>
                            <table width="100%" cellpadding="10" cellspacing="0" style="border-collapse: collapse; margin: 20px 0;">
                                <tr style="background-color: #f0f7f4;">
                                    <td style="border: 1px solid #dddddd; color: #333333;"><strong>Memorization Score</strong></td>
                                    <td style="border: 1px solid #dddddd; color: #333333;">{memorizationScore:F1} / 100</td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #dddddd; color: #333333;"><strong>Attendance Score</strong></td>
                                    <td style="border: 1px solid #dddddd; color: #333333;">{attendanceScore:F1} / 100</td>
                                </tr>
                                <tr style="background-color: #f0f7f4;">
                                    <td style="border: 1px solid #dddddd; color: #333333;"><strong>Participation Score</strong></td>
                                    <td style="border: 1px solid #dddddd; color: #333333;">{participationScore:F1} / 100</td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid #dddddd; color: #333333;"><strong>Behavior Score</strong></td>
                                    <td style="border: 1px solid #dddddd; color: #333333;">{behaviorScore:F1} / 100</td>
                                </tr>
                                <tr style="background-color: #3B666B;">
                                    <td style="border: 1px solid #dddddd; color: #ffffff;"><strong>Total Score</strong></td>
                                    <td style="border: 1px solid #dddddd; color: #ffffff;"><strong>{totalScore:F1} / 100</strong></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #f9f9f9; padding: 20px 30px; text-align: center; border-top: 1px solid #eeeeee;">
                            <p style="color: #999999; font-size: 12px; margin: 0;">This is an automated message, please do not reply.</p>
                            <p style="color: #999999; font-size: 12px; margin: 5px 0 0;">© 2025 Methaq. All rights reserved.</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
""";
    }
}