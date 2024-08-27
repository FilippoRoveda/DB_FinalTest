<?php
    $host = "bv0zgfxkyeyonnhqbynh-mysql.services.clever-cloud.com";
    $dbname = "bv0zgfxkyeyonnhqbynh";
    $user = "uqdg0lvhi13cwdds";
    $password = "YA2QwEPAL1S0UZBNx3bZ";

    $connection_String = "mysql:host=$host;dbname=$dbname;user=$user;password=$password";
    ob_start();
    
    try
    {
        $connection_Object = new PDO($connection_String);
        $connection_Object->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        echo "Connection to database successfull!";
    }
    catch(PDOException $e)
    {
        $exceptionMessage =  $e->getMessage();
        $errorMessage = "ERROR: Connection failed!";
        
        echo nl2br ("$exceptionMessage \n $errorMessage");
    }
?>