<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = pg_connect($auth_host, $auth_user, $auth_pass) or die (pg_error());
	pg_select_db($auth_dbase,$db);
	
	
	$username = pg_real_escape_string($_POST['name']);
	$password = pg_real_escape_string($_POST['password']);
	$email = pg_real_escape_string($_POST['email']);

	$sql = pg_query("SELECT * FROM account WHERE user = '$username'");
	$rows= pg_num_rows($sql);

	if($rows > 0){
		echo "false";
	}else{
		$activation = md5(uniqid(rand(), true));
		pg_query("INSERT INTO account(user,password,email) VALUES ('$username',MD5('".$password."'),'$email')");

		echo "true";	
	}
	pg_close($db);
?> 
 