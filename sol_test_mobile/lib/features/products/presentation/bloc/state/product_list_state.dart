import 'package:sol_test_mobile/features/products/domain/entities/product.dart';

sealed class ProductListState {}

final class ProductListInitial extends ProductListState {}

final class ProductListLoading extends ProductListState {}

final class ProductListLoaded extends ProductListState {
    final List<Product> products;
    final bool hasReachedMax;

    ProductListLoaded({
        required this.products, 
        this.hasReachedMax = false,
    });

    ProductListLoaded copyWith({
        List<Product>? products,
        bool? hasReachedMax,
    }) {
        return ProductListLoaded(
            products: products ?? this.products,
            hasReachedMax: hasReachedMax ?? this.hasReachedMax,
        );
    }
}

final class ProductListError extends ProductListState {
    final String message;
    ProductListError(this.message);
}