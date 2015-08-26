<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";

    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$user_name = $_POST['name'];
	$user_password = $_POST['password'];

	$sql = pg_query($db, "SELECT * FROM account WHERE user_id = '$user_name' AND password = MD5('".$user_password."') ") or die(pg_error());
	
	$rows= pg_num_rows($sql);
	if($rows > 0){
		echo "true";
	}else{
		echo "false";
	}
	pg_close($db);
	
?> 