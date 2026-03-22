import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/get_products_by_filter.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_list_state.dart';

class ProductListCubit extends Cubit<ProductListState> {
  final GetProductsByFilter getProductsByFilters;

  ProductListCubit({
    required this.getProductsByFilters,
  }) : super(ProductListInitial());

  Future<void> fetchProducts({String? query}) async {
    emit(ProductListLoading());
    try {
      final products = await getProductsByFilters(search: query);
      emit(ProductListLoaded(products: products));
    } catch (e) {
      emit(ProductListError("Error loading products"));
    }
  }
}