<?php

$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbname = "databass_wubwubwub";

$password = $_POST["passwordPost"];
$email = $_POST["emailPost"];

//Make a connection
$conn = new mysqli($servername, $server_username, $server_password, $dbname);
//Check connection
if (!$conn) {
    die("connection Failed.".mysql_connect_error());
}
$sql = "SELECT password FROM users WHERE email = '".$email."'";
$result = mysqli_query($conn, $sql);

if (!$result) {
    echo "error";
} else {
    $sqlUpdatePassword = "UPDATE users SET password = '".$password."' WHERE email = '".$email."'";
    $resultChangePassword = mysqli_query($conn, $sqlUpdatePassword);
    if (!$resultChangePassword) {
        echo "error";
    } else {
        echo "Password Changed";
    }
}
?>