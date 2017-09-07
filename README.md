# ASP.NET Core CSRF Mitigation Sample

## Introduction

Welcome to a sample application that shows three ways to mitigate CSRF attacks in ASP.NET Core applications:
    
  - SameSite cookies
  - Checking the Origin header
  - Leveraging built-in CSRF tokens

This site does not have a lot to offer but you can log in and edit your profile data. Here's the list of allowed logins:

  - `marcin`
  - `janek`
  - `franek`

The password is `s3cr3t`. Yes, all of the logins use the same password.

The main site is accompanied by an evil twin site that can be used to execute a CSRF attack!

## Prerequisites

The sample consists of two Web sites:

  - `http://web.local:57082/`
  - `http://evil.local:56326/`

Using these host names is important due to cookies being bound to `web.local`. Make sure you add the following entries to your hosts file:

```
127.0.0.1 web.local
127.0.0.1 evil.local
```

You may also have to add URL reservations for both sites:

```
C:\>netsh http add urlacl url=http://web.local:57082/ user=<USER>
C:\>netsh http add urlacl url=http://evil.local:56326/ user=<USER>
```
