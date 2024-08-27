<?php

require "01_DB_Connection.php";

ob_end_clean();

$lvl = $_POST['Level'];
$lvl = (int)$lvl;

if(filter_var(FILTER_VALIDATE_INT))
{
    if($lvl >= 0 && $lvl < 6)
    {
        if($lvl === 0)
        {
            $sql = "SELECT * FROM Runs;";
            $datas = $connection_Object->query($sql);
            $allUsers = $datas->fetchAll(PDO::FETCH_ASSOC);

            $json = json_encode($allUsers);
            echo  $json;
        }
        else
        {
            $sql = "SELECT * FROM Runs WHERE CurrentLevel = $lvl;";
            $datas = $connection_Object->query($sql);
            $allUsers = $datas->fetchAll(PDO::FETCH_ASSOC);

            $json = json_encode($allUsers);
            echo  $json;
        }
    }
    else
    {
        echo "ERROR: Please insert a valid number, 0 to 5!";
    }
}
else
{
    echo "ERROR: Please insert an integer!";
}

?>