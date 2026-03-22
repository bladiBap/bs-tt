
sealed class ProductDetailState {

}

final class ProductDetailInitial extends ProductDetailState {}
final class ProductDetailLoading extends ProductDetailState {}
final class ProductDetailLoaded extends ProductDetailState {
  final String name;
  final String sku;
  final double price;
  final String currencySymbol;

  ProductDetailLoaded({
    required this.name,
    required this.sku,
    required this.price,
    required this.currencySymbol,
  });
}

final class ProductDetailError extends ProductDetailState {
  final String message;

  ProductDetailError(this.message);
}