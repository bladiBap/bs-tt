import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/create_product.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_create_state.dart';

class CreateProductCubit extends Cubit<CreateProductState> {
  final CreateProduct createProduct;

  CreateProductCubit({
    required this.createProduct,
  }) : super(CreateProductInitial());

  Future<void> addProduct(String name, double price, String currencyId) async {
    emit(CreateProductLoading());

    try {
      final newProduct = await createProduct(
        name: name,
        price: price,
        currencyId: currencyId,
      );
      emit(CreateProductSuccess(newProduct));
    } catch (e) {
      emit(CreateProductError("Could not create product"));
    }
  }
  void reset() => emit(CreateProductInitial());
}