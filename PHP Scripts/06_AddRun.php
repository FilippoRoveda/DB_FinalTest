<?php

require "01_DB_Connection.php";

ob_end_clean();

$PlayerID = $_POST["PlayerID"];
$CurrentLevel = $_POST["CurrentLevel"];
$PlayTime = $_POST["PlayTime"];
$Score = $_POST["Score"];

try
{
    //$prepared_Query = $connection_Object->prepare("SELECT * FROM Users WHERE Username = :Username");
    //$prepared_Query->bindParam(':Username', $PlayerID);
    
    //$results = $prepared_Query->execute();
    
    //$row = $prepared_Query->fetch(PDO::FETCH_ASSOC);

    
    //if ($results && $PlayerID === $row['Username']) 
    //{
        if(($CurrentLevel < 6 && $CurrentLevel > 0) && filter_var($CurrentLevel, FILTER_VALIDATE_INT))
        {
            $prepared_Insertion = $connection_Object->prepare("INSERT INTO Runs VALUES (0, :PlayerID, :CurrentLevel, :PlayTime, :Score)");
            $prepared_Insertion-> bindParam(':PlayerID', $PlayerID);
            $prepared_Insertion-> bindParam(':CurrentLevel', $CurrentLevel);
            
            if(filter_var($PlayTime, FILTER_VALIDATE_FLOAT))
            {
                $FormattedPlayTime = gmdate("H:i:s", $PlayTime);
                $prepared_Insertion-> bindParam(':PlayTime', $FormattedPlayTime);
            }
            else
            {
                echo "ERROR: PlayTime insertion failed, please insert a numeric value";
            }
            
            if(filter_var($Score, FILTER_VALIDATE_INT))
            {
                $prepared_Insertion-> bindParam(':Score', $Score);
            }
            else
            {
                echo "ERROR: Score insertion failed, please insert an INTEGER number";
            }
            $prepared_Insertion->execute();
            echo "Run insertion successfull for user '$PlayerID";
        }
        else
        {
           echo "ERROR: Please insert level between 1 and 5."; 
        }
    //}
    //else
    //{
       //echo "ERROR: No player founded with username '$PlayerID'"; 
    //}
}
catch(PDOException $e)
    {
        $exceptionMessage =  $e->getMessage();
        $errorMessage = "ERROR: PlayerID autentication phase failed.";
        
        echo nl2br ("$errorMessage \n $exceptionMessage");
    }

?>