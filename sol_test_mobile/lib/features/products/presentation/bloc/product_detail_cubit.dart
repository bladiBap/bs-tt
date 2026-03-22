import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:sol_test_mobile/features/products/domain/usecases/get_product_by_id.dart';
import 'package:sol_test_mobile/features/products/presentation/bloc/state/product_detail_state.dart';

class ProductDetailCubit extends Cubit<ProductDetailState> {
    final GetProductById getProductById;

    ProductDetailCubit({required this.getProductById}) : super(ProductDetailInitial());

    Future<void> loadProduct(String id) async {
        emit(ProductDetailLoading());
        
        try {
            final product = await getProductById(id);

            emit(ProductDetailLoaded(
                name: product.name,
                sku: product.sku ?? "N/A",
                price: product.price,
                currencySymbol: product.currency!.symbol
            ));
        } catch (e) {
            emit(ProductDetailError("Can not load product details"));
        }
    }
}