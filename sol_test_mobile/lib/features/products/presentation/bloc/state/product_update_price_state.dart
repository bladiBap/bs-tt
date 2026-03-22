sealed class UpdatePriceProductState {}

final class UpdatePriceProductInitial extends UpdatePriceProductState {}
final class UpdatePriceProductLoading extends UpdatePriceProductState {}
final class UpdatePriceProductSuccess extends UpdatePriceProductState {}

final class UpdatePriceProductError extends UpdatePriceProductState {
    final String message;
    UpdatePriceProductError(this.message);
}