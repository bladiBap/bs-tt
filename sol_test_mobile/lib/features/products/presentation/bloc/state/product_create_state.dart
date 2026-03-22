import 'package:sol_test_mobile/features/products/domain/entities/product.dart';

sealed class CreateProductState {}

final class CreateProductInitial extends CreateProductState {}

final class CreateProductLoading extends CreateProductState {}

final class CreateProductSuccess extends CreateProductState {
    final Product newProduct;
    CreateProductSuccess(this.newProduct);
}

final class CreateProductError extends CreateProductState {
    final String message;
    CreateProductError(this.message);
}

final class CreateProductProcessing extends CreateProductState {}