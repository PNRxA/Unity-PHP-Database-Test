<?php

$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbname = "databass_wubwubwub";

$email = $_POST["emailPost"];

//Make a connection
$conn = new mysqli($servername, $server_username, $server_password, $dbname);
    //Check connection
if (!$conn) {
    die("connection Failed.".mysql_connect_error());
}
$sql = "SELECT username FROM users WHERE email = '".$email."'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    while ($row = mysqli_fetch_assoc($result)) {
        echo $row['username'];
    }
} else {
    echo "No User";
}
?>