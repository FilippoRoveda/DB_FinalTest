<?php

require "01_DB_Connection.php";

$Username = $_POST["Username"];
$Password = $_POST["Password"];
$LastPlay = date("Y-m-d H:i:s");

ob_end_clean();

try
{
    $prepared_Query = $connection_Object->prepare("SELECT * FROM Users WHERE Username = :Username");
    $prepared_Query->bindParam(':Username', $Username);
    
    $results = $prepared_Query->execute();
    
    if ($results) 
    {
        $row = $prepared_Query->fetch(PDO::FETCH_ASSOC);
        
        if(HASH('sha256', $Password) === $row['Password'])
        {
            $update = $connection_Object->prepare("UPDATE Users SET LastPlay='$LastPlay', IsLoggedIn=1 WHERE Username=:Username");
            $update->bindParam(':Username', $Username);
            
            $update->execute();
            
            
            echo "'$Username' is now logged.";
        }
        else
        {
                echo "ERROR: Incorrect password inserted.";
        }
    } 
    else 
    {
        echo 'ERROR: ' . mysql_error();
    }
}
catch(PDOException $e)
{
    echo $e->getMessage();
}
?>