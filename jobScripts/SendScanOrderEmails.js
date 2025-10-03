var mysql = require('mysql');
const nodemailer = require('nodemailer');

const NOT_FOUND_EMAIL = [
    "xiangyucona@hotmail.com"
]

var con = mysql.createConnection({
    host: "20.104.19.45",
    port: 3310,
    user: "root",
    password: "Pg89YnXRfFuhv@.",
    database: "yj"
});

const query = `
SELECT e.Id Id, o.DomesticNumber, s.Warehouse Warehouse, r.mailbox, e.DateCreated FROM email_data_in_warehouse e
JOIN transport_order o ON e.OrderId=o.Id
JOIN support_user s ON e.SenderUserId=s.UserId
JOIN user r ON RecipientUserId=r.Id
WHERE e.DateSentEmail IS NULL
`;

function _getTimeString(time) {
    time = new Date(time.setUTCHours(time.getUTCHours() + 8));
    const month = time.getUTCMonth() + 1;
    const date = time.getUTCDate();
    const hour = time.getUTCHours();
    const minute = time.getUTCMinutes();
    return `北京时间${month}月${date}日${hour}:${minute}`;
}

function GetEmailBody(orders) {
    let text = "壹嘉国际提醒您（按仓库排序）- Weekly report\n\n";
    for (let i = 0; i < orders.length; i++) {
        let time = orders[i].DateCreated;
        time = new Date(time.setUTCHours(time.getUTCHours() + 8));
        text = text + `${orders[i].DomesticNumber} ${orders[i].Warehouse} 已于 ${_getTimeString(orders[i].DateCreated)} 签收\n`;
    }
    text = text + "\n空运每5-10个工作日发货\n\n";
    text = text + "海运每10-25日发货\n\n";
    text = text + "直邮线路发货请货齐后系统内操作或联系客服";
    return text;
}

const transporter = nodemailer.createTransport({
    host: 'smtp.gmail.com',
    port: 587,
    auth: {
        user: 'notification.eplus@gmail.com',
        pass: 'dybqcagazakncdqb'
    }
});

con.query(query, async function (error, results, fields) {
    if (error) throw error;
    const mails = [...new Set(results.map(r => r.mailbox))];
    for (let i = 0; i < mails.length; i++) {
        let m = mails[i];
        if (!IsValidEmail(m)) {
            console.log("Invalid email: ", m);
            continue;
        }
        const orders = results.filter(r => r.mailbox == m).sort((a,b) => a.Warehouse > b.Warehouse ? 1 : -1 );
        let info = await transporter.sendMail({
            from: 'notification.eplus@gmail.com',
            to: m,
            subject: '已入库',
            text: GetEmailBody(orders)
        });
        
        con.query(`UPDATE email_data_in_warehouse SET DateSentEmail=NOW() WHERE Id IN (${orders.map(s => s.Id).join(',')})`)
        console.log("Sent to: " + m + ", " + orders.length + " orders.");
        await sleep(3000);
    }
    process.exit();
});

function sleep(ms) {
  return new Promise((resolve) => {
    setTimeout(resolve, ms);
  });
} 

function IsValidEmail(m) {
    if (!m) return false;
    let parts = m.split('@');
    if (parts.length != 2) return false;
    if (parts[0].trim() === '' || parts[1].trim() === '') return false;
    if (m.includes(' ')) return false;
    return true;
}