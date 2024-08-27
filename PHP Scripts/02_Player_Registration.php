<?php

    require "01_DB_Connection.php";

    $Name = $_POST["Name"];
    $Username = $_POST["Username"];
    $Password = $_POST["Password"];
    $FirstPlay = date("Y-m-d H:i:s");
    $LastPlay = date("0000-00-00 00:00:00");
    $TotalPlayTime = "000:00:00";
    
    ob_end_clean();
    
    try
    {
        $prepared_Insertion = $connection_Object->prepare("INSERT INTO Users VALUES (0, :Name, :Username, :Password, '$FirstPlay', '$LastPlay', '$TotalPlayTime', 0)");
        $prepared_Insertion-> bindParam(':Name', $Name);
        $prepared_Insertion-> bindParam(':Username', $Username);
        $Password = HASH('sha256', $Password);
        $prepared_Insertion-> bindParam(':Password', $Password);
        $prepared_Insertion->execute();
        echo "Registration successfull for '$Username'!";
      
    }
    catch(PDOException $e)
    {
        $exceptionMessage =  $e->getMessage();
        $errorMessage = "ERROR: Registration failed.";
        
        echo nl2br ("$errorMessage \n $exceptionMessage");
    }

?>