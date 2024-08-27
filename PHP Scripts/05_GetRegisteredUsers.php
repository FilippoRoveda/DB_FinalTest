<?php

require "01_DB_Connection.php";

ob_end_clean();

$sql = "SELECT ID, Username FROM Users;";
$datas = $connection_Object->query($sql);
$allUsers = $datas->fetchAll(PDO::FETCH_ASSOC);

$json = json_encode($allUsers);
echo  $json;

?>