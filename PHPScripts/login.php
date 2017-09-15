<?php

$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbname = "databass_wubwubwub";

$username = $_POST["usernamePost"];
$password = $_POST["passwordPost"];
//Make Connection
$conn = new mysqli($servername, $server_username, $server_password, $dbname);
//Check Connection
if (!$conn) {
    die("Connection Failed.". mysql_connect_error());
}
    $sql = "SELECT password FROM users WHERE username = '".$username."' ";
    $result = mysqli_query($conn, $sql);
    
    //get result and confirm match
if (mysqli_num_rows($result) > 0) {
    //show data for each row
    while ($row = mysqli_fetch_assoc($result)) {
        if ($row['password'] == $password) {
            echo "Login Success";
        } else {
            echo "Password incorrect";
        }
    }
} else {
    echo "User not found";
}
?>
