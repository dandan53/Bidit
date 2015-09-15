using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidit.Controllers
{
    class EmailTemplate
    {
        public static string ForgetPasswordEmail =

    @"<html>
        <head>
            <title></title>
        </head>
        <body>
            <table style='width:100%;height:auto;direction:rtl' cellpadding='0' cellspacing='0' border='0' width='100%' align='center'>
                <tbody>
            `     <tr>
                    <td width='100%' align='center' style='background-color:deepskyblue;padding:0px;max-height:80px'>
                        <!--<img src='pix/bid.jpg' alt='BidIt' border='0' style='max-width:137px;max-height:49px' img>-->
                        <h1><a href='http://bidit.apphb.com/'>בידיט</a></h1>
                    </td>
                 </tr>
                 <tr>
                    <td width='80%' align='right' style='padding:10px 0px 0px 0px'>
                        <table cellpadding='0' cellspacing='0' align='right' border='0' dir='rtl' width='100%'>
                            <tbody>
                                <tr>
                                    <td width='100%' align='right' style='font-size:13px;font-family:Arial;direction:rtl;font-size:11pt;padding:10px 0px 0px 15px;color:#505050'>
                                        <b>
                                            שלום USERNAME,
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:20px 0px 3px 0px'>
                                        להלן פרטי המשתמש שלך באתר <span class='il'>בידיט</span>:
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:15px 0px 5px 0px'>
                                        שם משתמש : <b>USERNAME</b></td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:0px 0px 10px 0px'>
                                        סיסמה : <b>PASSWORD</b></td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:10px 0px 5px 0px;line-height:20px'>
                                        כדי להחליף את הסיסמה יש להיכנס לעמוד 'ההגדרות שלי' וללחוץ על 'החלפת הסיסמא האישית שלי'.
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;color:#505050;direction:rtl;padding:25px 0px 3px 0px'>
                                        בברכה,
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;direction:rtl;color:#505050;padding:0px 0px 30px 0px'>
                                        צוות <span class='il'>בידיט</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    </tr>
                </tbody>
            </table>
        </body>
    </html>";


        public static string RegistrationEmail =

    @"<html>
        <head>
            <title></title>
        </head>
        <body>
            <table style='width:100%;height:auto;direction:rtl' cellpadding='0' cellspacing='0' border='0' width='100%' align='center'>
                <tbody>
            `     <tr>
                    <td width='100%' align='center' style='background-color:deepskyblue;padding:0px;max-height:80px'>
                        <!--<img src='pix/bid.jpg' alt='BidIt' border='0' style='max-width:137px;max-height:49px' img>-->
                        <h1><a href='http://bidit.apphb.com/'>בידיט</a></h1>
                    </td>
                 </tr>
                 <tr>
                    <td width='80%' align='right' style='padding:10px 0px 0px 0px'>
                        <table cellpadding='0' cellspacing='0' align='right' border='0' dir='rtl' width='100%'>
                            <tbody>
                                <tr>
                                    <td width='100%' align='right' style='font-size:13px;font-family:Arial;direction:rtl;font-size:11pt;padding:10px 0px 0px 15px;color:#505050'>
                                        <b>
                                            שלום USERNAME,
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:20px 0px 3px 0px'>
                                        ברכות על הצטרפותך לאתר <span class='il'>בידיט</span>.
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:20px 0px 3px 0px'>
                                        :להלן פרטי המשתמש שלך באתר
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:15px 0px 5px 0px'>
                                        שם משתמש : <b>USERNAME</b></td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:0px 0px 10px 0px'>
                                        סיסמה : <b>PASSWORD</b></td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:10px 0px 5px 0px;line-height:20px'>
                                        נשמח לעמוד לרשותך בטלפון 077-0-000-000
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;color:#505050;direction:rtl;padding:25px 0px 3px 0px'>
                                        בברכה,
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;direction:rtl;color:#505050;padding:0px 0px 30px 0px'>
                                        צוות <span class='il'>בידיט</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    </tr>
                </tbody>
            </table>
        </body>
    </html>";

        public static string BidEndedEmail =

           @"<html>
        <head>
            <title></title>
        </head>
        <body>
            <table style='width:100%;height:auto;direction:rtl' cellpadding='0' cellspacing='0' border='0' width='100%' align='center'>
                <tbody>
            `     <tr>
                    <td width='100%' align='center' style='background-color:deepskyblue;padding:0px;max-height:80px'>
                        <!--<img src='pix/bid.jpg' alt='BidIt' border='0' style='max-width:137px;max-height:49px' img>-->
                        <h1><a href='http://bidit.apphb.com/'>בידיט</a></h1>
                    </td>
                 </tr>
                 <tr>
                    <td width='80%' align='right' style='padding:10px 0px 0px 0px'>
                        <table cellpadding='0' cellspacing='0' align='right' border='0' dir='rtl' width='100%'>
                            <tbody>
                                <tr>
                                    <td width='100%' align='right' style='font-size:13px;font-family:Arial;direction:rtl;font-size:11pt;padding:10px 0px 0px 15px;color:#505050'>
                                        <b>
                                            שלום USERNAME,
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:20px 0px 3px 0px'>
                                        מכרז מספר BID_ID הסתיים.
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:20px 0px 3px 0px'>
                                        :להלן פרטי המכרז
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:15px 0px 5px 0px'>
                                        מוצר : <b>PRODUCT_NAME</b></td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:0px 0px 10px 0px'>
                                        המחיר הטוב ביותר : <b>BEST_PRICE</b> ₪</td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-family:Arial;font-size:11pt;direction:rtl;color:#505050;padding:10px 0px 5px 0px;line-height:20px'>
                                        נשמח לעמוד לרשותך בטלפון 077-0-000-000
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;color:#505050;direction:rtl;padding:25px 0px 3px 0px'>
                                        בברכה,
                                    </td>
                                </tr>
                                <tr>
                                    <td width='100%' align='right' style='font-size:11pt;font-family:Arial;direction:rtl;color:#505050;padding:0px 0px 30px 0px'>
                                        צוות <span class='il'>בידיט</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    </tr>
                </tbody>
            </table>
        </body>
    </html>";



    }
}