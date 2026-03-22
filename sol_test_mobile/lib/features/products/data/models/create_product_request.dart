class CreateProductRequest {
  final String name;
  final double price;
  final String currencyId;

  CreateProductRequest({
    required this.name,
    required this.price,
    required this.currencyId,
  });

  Map<String, dynamic> toJson() => {
    'name': name,
    'price': price,
    'currencyId': currencyId,
  };
}