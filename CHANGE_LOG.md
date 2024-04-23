# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]
### Added
- Change log file
- Support for custom query params
### Fixes
- Fixed an issue when trying to performa POST, PUT or PATCH requests without a request object. Now the plugin sends a error message to the console and returns a null response.
- Fixed a small bug with the request header construction

## [1.0.0] - 2024-03-17
### Added
- Simple interface to make HTTP requests
- Handle responses in a simple way
- Capable of making async GET, POST, PUT, PATCH and DELETE requests
- Supports custom Headers
- Supports custom Request body
- Can parse JSON responses into C# objects