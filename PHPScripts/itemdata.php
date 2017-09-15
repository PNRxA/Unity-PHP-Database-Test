<?php
/**
 * ItemData Class Doc Comment
 * PHP Version 7.1.7
 *
 * @category Class
 * @package  ItemData
 * @author   Peter Runham <pnrunham@gmail.com>
 * @license  http://www.gnu.org/copyleft/gpl.html GNU General Public License
 * @link     http://www.hashbangcode.com/
 */
$servername = "localhost";
$server_username = "root";
$server_password = "";
$dbname = "databass_wubwubwub";

//Make a connection
$conn = new mysqli($servername, $server_username, $server_password, $dbname);
//Check connection
if (!$conn) {
    die("connection Failed.".mysql_connect_error());
}

$sql = "SELECT id, 
        name,
        clipsize, 
        damage, 
        cooldown, 
        weaponrange, 
        weight, 
        ammotype, 
        iconname 
        FROM weapondb";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    //show data for each
    while ($row = mysqli_fetch_assoc($result)) {
        echo $row['id']."|".
            $row['name']."|".
            $row['clipsize']."|".
            $row['damage']."|".
            $row['cooldown']."|".
            $row['weaponrange']."|".
            $row['weight']."|".
            $row['ammotype']."|".
            $row['iconname'].";";
    }
}
?>