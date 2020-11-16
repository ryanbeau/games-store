using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class UpdateDiscountAndImagesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "CartGame",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "DiscountId", "DiscountFinish", "DiscountPrice", "DiscountStart", "GameId" },
                values: new object[,]
                {
                    { 7, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 59.99m, new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 27 },
                    { 8, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 13.69m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 9, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 48.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 10, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 14.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 11, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 52.79m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 12, new DateTime(2021, 7, 24, 23, 0, 0, 0, DateTimeKind.Unspecified), 7.99m, new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.InsertData(
                table: "GameImage",
                columns: new[] { "GameImageId", "GameId", "ImageType", "ImageURL" },
                values: new object[,]
                {
                    { 58, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_8f34a87469f3a0c73049cbd0469bdff6e3d22713.jpg" },
                    { 145, 29, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_15f5759c441e4e5f51e1a8ee333e4ab9df9aa783.jpg" },
                    { 144, 29, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_271f850eec3f96b22aa17be35b948268e0771c7f.jpg" },
                    { 143, 29, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_d09106060fb7de8bf342c23df18b14debc8a15a3.jpg" },
                    { 142, 28, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_2be9153a2633e671c283e2dbcec64e2e4543f66f.jpg" },
                    { 141, 28, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_f501156a69223131ee8b12452f3003698334e964.jpg" },
                    { 140, 28, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_cf53258cb8c4d283e52cf8dce3edf8656f83adc6.jpg" },
                    { 139, 28, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/289070/ss_36c63ebeb006b246cb740fdafeb41bb20e3b330d.jpg" },
                    { 138, 27, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_d1a8f5a69155c3186c65d1da90491fcfd43663d9.jpg" },
                    { 137, 27, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_4ce07ae360b166f0f650e9a895a3b4b7bf15e34f.jpg" },
                    { 136, 27, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_668dafe477743f8b50b818d5bbfcec669e9ba93e.jpg" },
                    { 135, 27, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_bac60bacbf5da8945103648c08d27d5e202444ca.jpg" },
                    { 134, 27, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1174180/ss_66b553f4c209476d3e4ce25fa4714002cc914c4f.jpg" },
                    { 146, 29, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1151640/ss_9db45aa04e8c8b5043b479f42ed36296bfc3a918.jpg" },
                    { 133, 26, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_943c487e302fa5fc303d59e45a03218e25a2a59c.jpg" },
                    { 131, 26, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_649e19ff657fa518d4c2b45bed7ffdc4264a4b3a.jpg" },
                    { 130, 26, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_c80d2f3fab624b18d9531adc6957767a7fede100.jpg" },
                    { 129, 25, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_ca56bd7c7cf60337f169d29115aa3f761422f551.jpg" },
                    { 128, 25, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_48bb8b8874175b55a0661d242eec349cdaf5bad0.jpg" },
                    { 127, 25, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_b81a5412d6d1edb80b7f90899a63c42043f5abcb.jpg" },
                    { 126, 25, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/245620/ss_d254f02db9453c6931b0850a62341d8651329095.jpg" },
                    { 125, 24, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_7da80363f25c7d7ccc95e7bfdc5d8091e0cea77e.jpg" },
                    { 124, 24, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_700fb95c4cfc073b2e0640dc5049a0df1c2940f4.jpg" },
                    { 123, 24, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_e720137320c342a6664e195bc875613f338397ca.jpg" },
                    { 122, 24, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1262540/ss_76eb69e48a6a0cc256603c2aa0844e5e6d5c8168.jpg" },
                    { 121, 23, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_2ca3198a526321a83deac259ba02f8690f64db88.jpg" },
                    { 120, 23, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_ff04f7cbf0ad72b618ab6e17f6af2a424cbf5eaa.jpg" },
                    { 132, 26, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/945360/ss_7ccc894b8f95091f608fa012450549091cce2423.jpg" },
                    { 57, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_846fd1166a03b1d3147618aaba1ff7ef4477085d.jpg" },
                    { 147, 30, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_f7861bd71e6c0c218d8ff69fb1c626aec0d187cf.jpg" },
                    { 149, 30, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_6834be966451a9b0f12eb4f68bfb0853ea0b7267.jpg" },
                    { 31, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_b70d5295ecc738dc2279797ec8351ac0fdf139f4.jpg" },
                    { 32, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_2082e17ee1dad2004c48e689a005f8f684f5b645.jpg" },
                    { 33, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_68aea6cce2261f8cdd52fe39bc1fceddc783ed69.jpg" },
                    { 34, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_cf79030615e1188ed7e31f36c6b4dcf8b5cf4512.jpg" },
                    { 35, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_ec04158ea75cfac3ea2703d00ab06cd8dfd4416a.jpg" },
                    { 36, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_f30c62166abbbb87b1c4a822f46b538d84978816.jpg" },
                    { 37, 1, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/227300/ss_3424d0fd16eaefeeef2d3e601db52dc62dc73c63.jpg" },
                    { 38, 2, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_02f624e7b929d29632416474c90fc9b046f9e9fc.jpg" },
                    { 39, 2, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_b38af388931fd2a652c5849a6d4ef25dbedf4645.jpg" },
                    { 40, 2, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_1478e30c80afd1ce72f6c6887691b76769130666.jpg" },
                    { 41, 2, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1239520/ss_f410761ffd30001b88a964356abad036a5ece574.jpg" },
                    { 42, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_09bb6bdce4fa085c2c4a9a8f48ea52d3051b44bc.jpg" },
                    { 148, 30, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_c310f858e6a7b02ffa21db984afb0dd1b24c1423.jpg" },
                    { 43, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_26b6414b4ae07cfc2e2d15bd6ff315a4678f00f3.jpg" },
                    { 45, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_0167fbdf9d30407734baf3ab3b08213945738166.jpg" },
                    { 46, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_38d4d1b21050fc4b3978fcf65c909260d3673fb7.jpg" },
                    { 47, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_1d3d9b7d9d752666feb9853215c118104816eee2.jpg" },
                    { 48, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_9868ee40f39749a4c8222502cf86525ee32c1bef.jpg" },
                    { 49, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_624638e46ed590d4bb1835558a5ab0981f7baadd.jpg" },
                    { 50, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_3531d9f91265d94fc06f6587eba1ca49f2c423d1.jpg" },
                    { 51, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_6f1836277ffe8733503a9446d51b8c7eb3d20d5f.jpg" },
                    { 52, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_bd8b719de92cfc9e65cd96d5da74426918964291.jpg" },
                    { 53, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_f983c0c1cc566b8ca21a6c45e6f044b57aff0f0f.jpg" },
                    { 54, 4, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/397540/ss_bb22ac18c1db1a87d779db0c3fb480eb1ce79f0e.jpg" },
                    { 55, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_65861a8ea2efcb01fca8aa4b1233663bb053ab54.jpg" },
                    { 150, 30, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/377160/ss_cd83d21b82e4c4e9a6d76edc98a8c2b70b1b5e9d.jpg" },
                    { 44, 3, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/219740/ss_3d32a04e77363f3c8179a319de6f90ac1b8b2e0e.jpg" },
                    { 118, 23, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_57b07abe4e9a1165da440a4b798f4383e560a2f7.jpg" },
                    { 119, 23, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/256330/ss_dc30c74424a74426194c924a6326e8ab9d2e6dc4.jpg" },
                    { 116, 22, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_da66e194128088e46a5ecad2af455ae2ebe84be0.jpg" },
                    { 84, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_eed79518c510b7b8ce6fce9d1c350bfcea530993.jpg" },
                    { 83, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_842bfdeb44368babd55ad93af1cbbf560f9fb9a1.jpg" },
                    { 82, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_c5c810244ba6dd899de44121b37d87fad2621d4c.jpg" },
                    { 81, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_6e7911d3622f3a801aeea1c2f8418f0f22880bb0.jpg" },
                    { 80, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_a85a01aae90c8ca69a05d32a1196ffaf6d943bd7.jpg" },
                    { 79, 9, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_7d9dc6d7c7362bca257e035e2fe161c68005b9f2.jpg" },
                    { 78, 9, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_066491f3039cd9919a8b1d3538d33100fe87dfca.jpg" },
                    { 77, 9, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_082575907197c03f4e27816f1fca1bd7d5c97848.jpg" },
                    { 76, 9, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/17450/ss_c79860e70c98e653453f4dc227df5820d9f841ca.jpg" },
                    { 75, 8, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_9fc2032db736a2a9919aba739566a8353c2cd3cb.jpg" },
                    { 74, 8, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_f93547d532f3b0b19dbc23a1500fd313f3619e03.jpg" },
                    { 73, 8, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_f258c8ebef5e2bf47a2feca529a2cd3f864cfbb0.jpg" },
                    { 117, 22, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_48be777780be9babf0ffb6c77766d5b0776adc1f.jpg" },
                    { 72, 8, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220200/ss_c5b19aad79ad37ba36708f22742c1bf81f9220ca.jpg" },
                    { 70, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_9fe75fccba08bfb18338f8833b030775a30b3685.jpg" },
                    { 69, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_f280252d02632345eeba8877ece485c8582dd30d.jpg" },
                    { 68, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_e7dc933b7e6a321f6be37367fce68a39dac26d16.jpg" },
                    { 67, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_62f421579193a15026c439f6c1685a28017b84ba.jpg" },
                    { 66, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_cbf706e7a6860429d148e49220dddf7ecac20cf7.jpg" },
                    { 65, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_922d27a19a98dd259e23b6f82901728da1e91bb8.jpg" },
                    { 64, 6, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_b898b51f69d795b804374bb6396c7c24b23545d3.jpg" },
                    { 63, 6, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_909a95f11266fed10eba4282b36608a9e731a1c5.jpg" },
                    { 62, 6, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_ac7dbb6b5d1353ec1e66110fe652883b957a70e3.jpg" },
                    { 61, 6, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/22490/ss_ec8a28942fcb5cb15718f949ab81124932a5084d.jpg" },
                    { 60, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_b49a25cb879b6dbaa6f851eae3b5bf6d3fc04478.jpg" },
                    { 59, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_cd920308517efb19c11b44e251af89e40fb412d5.jpg" },
                    { 71, 7, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/346010/ss_4c0020e484b09452631df8cfc42a97d9d1ddb0c1.jpg" },
                    { 86, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_0c08ce5d7f63fd8cee404426f6d083d93105a924.jpg" },
                    { 85, 10, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/220240/ss_83af36773eb0393144cfefa80ceb6baf5c608cf7.jpg" },
                    { 88, 11, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_3210874e1d6e20965cc9c76ac2d7e899ef2b0b9f.jpg" },
                    { 115, 22, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_16688139333e39593af0eccc77342165eacae0d0.jpg" },
                    { 114, 22, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/306130/ss_c1cdae9879550709486774eed3a2760d18955349.jpg" },
                    { 113, 21, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_a9642404e586be28f856e8f02d038828f691a5ba.jpg" },
                    { 112, 21, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_c142f5078ace9f5e2eb2c80aa3bf768e156b4ee9.jpg" },
                    { 111, 21, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_d923711c0eb833b1df8607fa3df4dcebbe470cf2.jpg" },
                    { 110, 21, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/1085660/ss_7fcc82f468fcf8278c7ffa95cebf949bfc6845fc.jpg" },
                    { 109, 18, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_5b88f071939e32c790dd9e84890d9c197956a7be.jpg" },
                    { 108, 18, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_5c4f1ad866b43b0d2aa18400216eb4e6168357b4.jpg" },
                    { 107, 18, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_25db57e715957f9673a69723867942f79b8357d9.jpg" },
                    { 106, 18, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/976310/ss_8f3f4ef12a5cfae47d8e768778c329f17ee8b320.jpg" },
                    { 105, 17, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_e8d08712cfffe7ec54889f12588c2c150537294e.jpg" },
                    { 104, 17, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_ba4f48d62ce18649310c16d11b80c33003c39886.jpg" },
                    { 56, 5, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/203160/ss_c93f9a97f2fd890f21c829cf8781850484eec7f3.jpg" },
                    { 102, 17, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_0ccc388fb5712016814e66dfa712285d13529bbc.jpg" },
                    { 101, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_2e80c1d9b6f0f6bfdbbff18b84baca2d1794cd7c.jpg" },
                    { 100, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_7dde72f87afe373d4624f49bf81575f8aa2a80fd.jpg" },
                    { 99, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_72a4d284ccc4535d23ba4e12752b8e5a3939b88e.jpg" },
                    { 89, 11, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_4670702422e948a9666c29814ac6cfdb941c5a4a.jpg" },
                    { 90, 11, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_040958f3839e7d1c3ae50c76200d0891c5ca6883.jpg" },
                    { 87, 11, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/233270/ss_602b7c88ab725ccabfd8ad7c94fb536875c329ad.jpg" },
                    { 92, 15, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_cd721eb1856f0dd3b820e4e998c3b5fe7e7c9b4e.jpg" },
                    { 93, 15, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_f64515607fd627aa9436be3b15fdcb9e1c89bb19.jpg" },
                    { 91, 15, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_bb2ee3b9b48a60857873192cfff10546e01d4a86.jpg" },
                    { 95, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_5339a74c4563d40a1d8a5638db2a9ed59c5b883b.jpg" },
                    { 96, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_d923566424cee1c6d82ccde7336b02057d3409fc.jpg" },
                    { 97, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_633faaacb74e2da5baa356e0f18f9f73e777c4d2.jpg" },
                    { 98, 16, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/433340/ss_e43a384a5d5ab9bcb943423c1e264a54ce840c43.jpg" },
                    { 94, 15, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/271590/ss_95a1f04eb687eae71478c0c5ba644da57e10f215.jpg" },
                    { 103, 17, 0, "https://cdn.cloudflare.steamstatic.com/steam/apps/252950/ss_868ba56dd81b7622bd9ba7f878fd143d0900bcfd.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "db58e7b1-100a-4eeb-909f-a9ddba8992ee");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4b58576f-8b3f-4be6-89ab-0a850de055c6");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a1b37c6-d79b-47df-b501-0bab422dd9ae", "ee4f65b8-2ad9-4db1-b365-947103accb1c", "AQAAAAEAACcQAAAAEMoG6XTCcfl4dtPp7DvuSu9NFLTq4WeY3akG4aatacd93gwwmv8NCSxmyA4m2iqj0A==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef9a4c31-0a2a-481e-94ff-accaba908851", "939a14f3-912c-410d-9a30-7a3c9210de09", "AQAAAAEAACcQAAAAEOrljZq+qIcjZt8yJKi5cGTJ8Ddfu7ot0YP5eyTf0LtKuZJfHkpd67tASfmidyrX5g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Discount",
                keyColumn: "DiscountId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "GameImage",
                keyColumn: "GameImageId",
                keyValue: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "CartGame",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "36bfd365-727c-4281-b0c7-86ac9e5030c5");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec5f42d7-549a-44ac-a2f3-d3fd6d21dde3");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cb9ba883-fc9a-4a32-afb6-5f974dfe1e20", "15374399-42dc-4b5d-9e4d-4b3262b18191", "AQAAAAEAACcQAAAAELNPWcioO8ZhqyHVS2oi0rsDdnYMrS9mgz/FEik5eTl0xzNCGas6pooT41P/aUP7Rw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74e5c8b8-46a9-4b6f-9512-0ec81bcb5272", "bdb1b8d3-0afa-415c-9fc6-d19dca24a9b7", "AQAAAAEAACcQAAAAEFfZnXn4icWKa3Bv70XSkpG8WRIcMBbL1pneYq7ADnfuBRhFjxdzbAIvMkKWltHFSw==" });
        }
    }
}
