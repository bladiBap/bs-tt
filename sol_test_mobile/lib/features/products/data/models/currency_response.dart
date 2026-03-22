import 'package:sol_test_mobile/features/products/domain/entities/currency.dart';

class CurrencyResponse {
  final String id;
  final String symbol;

  CurrencyResponse({
    required this.id,
    required this.symbol
  });

  factory CurrencyResponse.fromJson(Map<String, dynamic> json) => 
    CurrencyResponse(
      id: json['id'],
      symbol: json['symbol'],
    );
    
  Currency toEntity() => Currency(id: id, symbol: symbol);
}