import 'package:dio/dio.dart';

class DioClient {
    late final Dio _dio;

    DioClient() {
        _dio = Dio(
        BaseOptions(
            baseUrl: 'https://unbitter-jeffry-monographical.ngrok-free.dev/api/v1',
            connectTimeout: const Duration(seconds: 15),
            receiveTimeout: const Duration(seconds: 15),
            responseType: ResponseType.json,
            headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            },
        ),
    );
  }

  Dio get instance => _dio;
}