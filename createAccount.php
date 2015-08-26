<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];
    $auth_port = $GLOBALS['auth_port'];

    $strConn = "host=$auth_host port=$auth_port dbname=$auth_dbase user=$auth_user password=$auth_pass";

    $db = pg_connect($strConn) or die ("Error connection: " .pg_last_error());
	
	$username = $_POST['name'];
	$password = $_POST['password'];
	$email = $_POST['email'];

	$sql = pg_query($db, "SELECT * FROM account WHERE user = '$username'");
	$rows= pg_num_rows($sql);

	if($rows > 0){
		echo "false";
	}else{
		$activation = md5(uniqid(rand(), true));
		pg_query($db, "INSERT INTO account(user_id,password,email) VALUES ('$username',MD5('".$password."'),'$email')");

		echo "true";	
	}
	pg_close($db);
?> 