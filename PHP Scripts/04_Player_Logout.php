<?php

require "01_DB_Connection.php";

$Username = $_POST["Username"];
$LastPlay = date("Y-m-d H:i:s");
$SessionLenght = $_POST["SessionLenght"];

ob_end_clean();

try
{
    $query = $connection_Object->prepare("SELECT TotalPlayTime FROM Users WHERE Username = :Username");
    $query->bindParam(':Username', $Username);
    $query->execute();
    $res = $query->fetch(PDO::FETCH_ASSOC);
    $playedTime = $res['TotalPlayTime'];


    $oldPlayedTime = preg_replace("/^([\d]{1,2})\:([\d]{2})$/", "00:$1:$2", $playedTime);

    sscanf($oldPlayedTime, "%d:%d:%d", $hours, $minutes, $seconds);

    $time_seconds = $hours * 3600 + $minutes * 60 + $seconds;
    
    $time_seconds = $time_seconds + $SessionLenght;
    
    $newTotalTimePlayed = gmdate("H:i:s", $time_seconds);
    
    $stmnt = $connection_Object->prepare("UPDATE Users SET LastPlay = '$LastPlay', IsLoggedIn = 0, TotalPlayTime = '$newTotalTimePlayed' WHERE Username = :Username AND IsLoggedIn = 1");
    
    $stmnt->bindParam(':Username', $Username);
    $stmnt->execute();
    
    echo "$Username un logged successfully!";

}
catch(PDOException $e)
{
    $exceptionMessage = $e->getMessage();
    echo nl2br ("ERROR: \n $exceptionMessage");
}

?>