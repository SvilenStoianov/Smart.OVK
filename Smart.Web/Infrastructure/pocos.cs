using System;
using LinqToDB.Mapping;

namespace Smart.Web.Infrastructure {
[Table(Name = "act")]
public partial class act {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_client")]
public int? ID_client { get; set; }
[Column(Name = "ID_hosp_daily")]
public int? ID_hosp_daily { get; set; }
[Column(Name = "TD_created")]
public DateTime? TD_created { get; set; }
[Column(Name = "ID_user_created")]
public int? ID_user_created { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "archive")]
public int? archive { get; set; }
[Column(Name = "ID_user_arch"), NotNull]
public string ID_user_arch { get; set; }
[Column(Name = "TD_archive")]
public DateTime? TD_archive { get; set; }
[Column(Name = "first_open")]
public int? first_open { get; set; }
[Column(Name = "TD_next")]
public DateTime? TD_next { get; set; }
[Column(Name = "final")]
public int? final { get; set; }
[Column(Name = "ID_act_type_next"), NotNull]
public string ID_act_type_next { get; set; }
[Column(Name = "payed")]
public int? payed { get; set; }
[Column(Name = "ID_user_payed")]
public int? ID_user_payed { get; set; }
[Column(Name = "TD_payed")]
public DateTime? TD_payed { get; set; }
[Column(Name = "total")]
public decimal? total { get; set; }
[Column(Name = "total_man")]
public decimal? total_man { get; set; }
[Column(Name = "gift")]
public int? gift { get; set; }
[Column(Name = "anulated")]
public int? anulated { get; set; }
[Column(Name = "TD_anulated")]
public DateTime? TD_anulated { get; set; }
[Column(Name = "ID_user_anulated")]
public int? ID_user_anulated { get; set; }
[Column(Name = "anul_reason"), NotNull]
public string anul_reason { get; set; }
[Column(Name = "round")]
public decimal? round { get; set; }
[Column(Name = "note_next"), NotNull]
public string note_next { get; set; }
[Column(Name = "payLock"), NotNull]
public int payLock { get; set; }
[Column(Name = "weight"), NotNull]
public string weight { get; set; }
[Column(Name = "ID_user_surrend"), NotNull]
public int ID_user_surrend { get; set; }
[Column(Name = "publicNote"), NotNull]
public string publicNote { get; set; }
}

[Table(Name = "act_calc_man")]
public partial class act_calc_man {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idUser"), NotNull]
public int idUser { get; set; }
[Column(Name = "idMan"), NotNull]
public int idMan { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
}

[Table(Name = "act_calc_store")]
public partial class act_calc_store {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idUser"), NotNull]
public int idUser { get; set; }
[Column(Name = "idArt"), NotNull]
public int idArt { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "qty"), NotNull]
public decimal qty { get; set; }
}

[Table(Name = "act_exam")]
public partial class act_exam {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act")]
public int? ID_act { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "ID_exam_type")]
public int? ID_exam_type { get; set; }
[Column(Name = "diag_descr"), NotNull]
public string diag_descr { get; set; }
[Column(Name = "symp_descr"), NotNull]
public string symp_descr { get; set; }
[Column(Name = "add_info"), NotNull]
public string add_info { get; set; }
[Column(Name = "nota_bene"), NotNull]
public string nota_bene { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "price_man")]
public decimal? price_man { get; set; }
[Column(Name = "price_man_perc")]
public int? price_man_perc { get; set; }
[Column(Name = "TD_man_perc")]
public DateTime? TD_man_perc { get; set; }
[Column(Name = "ID_user_man_perc")]
public int? ID_user_man_perc { get; set; }
[Column(Name = "locked")]
public int? locked { get; set; }
}

[Table(Name = "act_files")]
public partial class act_files {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act"), NotNull]
public int ID_act { get; set; }
[Column(Name = "fname"), NotNull]
public string fname { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
}

[Table(Name = "act_imaging")]
public partial class act_imaging {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act"), NotNull]
public int ID_act { get; set; }
[Column(Name = "ID_type")]
public int? ID_type { get; set; }
[Column(Name = "TD_created")]
public DateTime? TD_created { get; set; }
[Column(Name = "ID_user_created")]
public int? ID_user_created { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "photo"), NotNull]
public string photo { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "price_man")]
public decimal? price_man { get; set; }
[Column(Name = "price_man_perc")]
public int? price_man_perc { get; set; }
[Column(Name = "TD_man_perc")]
public DateTime? TD_man_perc { get; set; }
[Column(Name = "ID_user_man_perc")]
public int? ID_user_man_perc { get; set; }
}

[Table(Name = "act_lab")]
public partial class act_lab {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act")]
public int? ID_act { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "api_key"), NotNull]
public string api_key { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "price_man")]
public decimal? price_man { get; set; }
[Column(Name = "price_man_perc")]
public int? price_man_perc { get; set; }
[Column(Name = "TD_man_perc")]
public DateTime? TD_man_perc { get; set; }
[Column(Name = "ID_user_man_perc")]
public int? ID_user_man_perc { get; set; }
[Column(Name = "jorderid"), NotNull]
public int jorderid { get; set; }
}

[Table(Name = "act_lab_photo")]
public partial class act_lab_photo {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_lab")]
public int? ID_lab { get; set; }
[Column(Name = "photo"), NotNull]
public string photo { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
}

[Table(Name = "act_vaccine")]
public partial class act_vaccine {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act")]
public int? ID_act { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "ID_vaccine")]
public int? ID_vaccine { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "price_man")]
public decimal? price_man { get; set; }
[Column(Name = "price_man_perc")]
public int? price_man_perc { get; set; }
[Column(Name = "TD_man_perc")]
public DateTime? TD_man_perc { get; set; }
[Column(Name = "ID_user_man_perc")]
public int? ID_user_man_perc { get; set; }
}

[Table(Name = "cards")]
public partial class cards {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "passkey"), NotNull]
public string passkey { get; set; }
[Column(Name = "status")]
public int? status { get; set; }
[Column(Name = "deleted")]
public int? deleted { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "printed")]
public int? printed { get; set; }
[Column(Name = "lot")]
public int? lot { get; set; }
[Column(Name = "in_count")]
public int? in_count { get; set; }
}

[Table(Name = "cash_avance")]
public partial class cash_avance {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_client"), NotNull]
public int ID_client { get; set; }
[Column(Name = "amm"), NotNull]
public decimal amm { get; set; }
[Column(Name = "used")]
public decimal? used { get; set; }
[Column(Name = "TD_in")]
public DateTime? TD_in { get; set; }
[Column(Name = "ID_user_in")]
public int? ID_user_in { get; set; }
[Column(Name = "used_hist"), NotNull]
public string used_hist { get; set; }
}

[Table(Name = "cash_avance_uses")]
public partial class cash_avance_uses {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act"), NotNull]
public int ID_act { get; set; }
[Column(Name = "ID_hosp"), NotNull]
public int ID_hosp { get; set; }
[Column(Name = "amm"), NotNull]
public decimal amm { get; set; }
}

[Table(Name = "clients_region")]
public partial class clients_region {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
}

[Table(Name = "clients_status")]
public partial class clients_status {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
}

[Table(Name = "exam_diag")]
public partial class exam_diag {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
}

[Table(Name = "exam_diag_sympth")]
public partial class exam_diag_sympth {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_diag")]
public int? ID_diag { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
}

[Table(Name = "exam_types")]
public partial class exam_types {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
[Column(Name = "color"), NotNull]
public string color { get; set; }
[Column(Name = "use_color")]
public int? use_color { get; set; }
}

[Table(Name = "help")]
public partial class help {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "cont"), NotNull]
public string cont { get; set; }
}

[Table(Name = "hospital")]
public partial class hospital {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_client")]
public int? ID_client { get; set; }
[Column(Name = "api_key"), NotNull]
public string api_key { get; set; }
[Column(Name = "TD_begin")]
public DateTime? TD_begin { get; set; }
[Column(Name = "TD_end")]
public DateTime? TD_end { get; set; }
[Column(Name = "ID_user_begin")]
public int? ID_user_begin { get; set; }
[Column(Name = "ID_user_end")]
public int? ID_user_end { get; set; }
[Column(Name = "TD_next")]
public DateTime? TD_next { get; set; }
[Column(Name = "ID_act_type_next"), NotNull]
public string ID_act_type_next { get; set; }
[Column(Name = "payed")]
public int? payed { get; set; }
[Column(Name = "ID_user_payed")]
public int? ID_user_payed { get; set; }
[Column(Name = "TD_payed")]
public DateTime? TD_payed { get; set; }
}

[Table(Name = "hospital_daily")]
public partial class hospital_daily {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_hosp"), NotNull]
public int ID_hosp { get; set; }
[Column(Name = "api_key"), NotNull]
public string api_key { get; set; }
[Column(Name = "TD_created")]
public DateTime? TD_created { get; set; }
[Column(Name = "ID_user_created")]
public int? ID_user_created { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "doc_date")]
public DateTime? doc_date { get; set; }
[Column(Name = "diag"), NotNull]
public string diag { get; set; }
[Column(Name = "symp_descr"), NotNull]
public string symp_descr { get; set; }
[Column(Name = "notes"), NotNull]
public string notes { get; set; }
[Column(Name = "owner_contact"), NotNull]
public string owner_contact { get; set; }
[Column(Name = "sum_consum"), NotNull]
public decimal sum_consum { get; set; }
[Column(Name = "sum_food"), NotNull]
public decimal sum_food { get; set; }
[Column(Name = "sum_hospital"), NotNull]
public decimal sum_hospital { get; set; }
[Column(Name = "weight"), NotNull]
public string weight { get; set; }
[Column(Name = "ID_user_response"), NotNull]
public int ID_user_response { get; set; }
[Column(Name = "ID_user_surrend"), NotNull]
public int ID_user_surrend { get; set; }
[Column(Name = "ID_user_payed")]
public int? ID_user_payed { get; set; }
[Column(Name = "TD_payed")]
public DateTime? TD_payed { get; set; }
[Column(Name = "payed")]
public int? payed { get; set; }
[Column(Name = "total")]
public decimal? total { get; set; }
[Column(Name = "anulated")]
public int? anulated { get; set; }
[Column(Name = "TD_anulated")]
public DateTime? TD_anulated { get; set; }
[Column(Name = "ID_user_anulated")]
public int? ID_user_anulated { get; set; }
[Column(Name = "anul_reason"), NotNull]
public string anul_reason { get; set; }
[Column(Name = "round")]
public decimal? round { get; set; }
}

[Table(Name = "hospital_daily_matrix")]
public partial class hospital_daily_matrix {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_hosp_daily"), NotNull]
public int ID_hosp_daily { get; set; }
[Column(Name = "fixed"), NotNull]
public int @fixed { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "add1"), NotNull]
public string add1 { get; set; }
[Column(Name = "add2"), NotNull]
public string add2 { get; set; }
[Column(Name = "p0800"), NotNull]
public string p0800 { get; set; }
[Column(Name = "p0900"), NotNull]
public string p0900 { get; set; }
[Column(Name = "p1000"), NotNull]
public string p1000 { get; set; }
[Column(Name = "p1100"), NotNull]
public string p1100 { get; set; }
[Column(Name = "p1200"), NotNull]
public string p1200 { get; set; }
[Column(Name = "p1300"), NotNull]
public string p1300 { get; set; }
[Column(Name = "p1400"), NotNull]
public string p1400 { get; set; }
[Column(Name = "p1500"), NotNull]
public string p1500 { get; set; }
[Column(Name = "p1600"), NotNull]
public string p1600 { get; set; }
[Column(Name = "p1700"), NotNull]
public string p1700 { get; set; }
[Column(Name = "p1800"), NotNull]
public string p1800 { get; set; }
[Column(Name = "p1900"), NotNull]
public string p1900 { get; set; }
[Column(Name = "p2000"), NotNull]
public string p2000 { get; set; }
[Column(Name = "p2200"), NotNull]
public string p2200 { get; set; }
[Column(Name = "p0000"), NotNull]
public string p0000 { get; set; }
[Column(Name = "p0200"), NotNull]
public string p0200 { get; set; }
[Column(Name = "p0400"), NotNull]
public string p0400 { get; set; }
[Column(Name = "p0600"), NotNull]
public string p0600 { get; set; }
[Column(Name = "p0800_note"), NotNull]
public string p0800_note { get; set; }
[Column(Name = "p0900_note"), NotNull]
public string p0900_note { get; set; }
[Column(Name = "p1000_note"), NotNull]
public string p1000_note { get; set; }
[Column(Name = "p1100_note"), NotNull]
public string p1100_note { get; set; }
[Column(Name = "p1200_note"), NotNull]
public string p1200_note { get; set; }
[Column(Name = "p1300_note"), NotNull]
public string p1300_note { get; set; }
[Column(Name = "p1400_note"), NotNull]
public string p1400_note { get; set; }
[Column(Name = "p1500_note"), NotNull]
public string p1500_note { get; set; }
[Column(Name = "p1600_note"), NotNull]
public string p1600_note { get; set; }
[Column(Name = "p1700_note"), NotNull]
public string p1700_note { get; set; }
[Column(Name = "p1800_note"), NotNull]
public string p1800_note { get; set; }
[Column(Name = "p1900_note"), NotNull]
public string p1900_note { get; set; }
[Column(Name = "p2000_note"), NotNull]
public string p2000_note { get; set; }
[Column(Name = "p2200_note"), NotNull]
public string p2200_note { get; set; }
[Column(Name = "p0000_note"), NotNull]
public string p0000_note { get; set; }
[Column(Name = "p0200_note"), NotNull]
public string p0200_note { get; set; }
[Column(Name = "p0400_note"), NotNull]
public string p0400_note { get; set; }
[Column(Name = "p0600_note"), NotNull]
public string p0600_note { get; set; }
[Column(Name = "ID_art")]
public int? ID_art { get; set; }
[Column(Name = "qty")]
public decimal? qty { get; set; }
[Column(Name = "ID_med_app")]
public int? ID_med_app { get; set; }
[Column(Name = "app_note"), NotNull]
public string app_note { get; set; }
}

[Table(Name = "hospital_files")]
public partial class hospital_files {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_hospital")]
public int? ID_hospital { get; set; }
[Column(Name = "fname"), NotNull]
public string fname { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
}

[Table(Name = "imaging_types")]
public partial class imaging_types {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
}

[Table(Name = "logger")]
public partial class logger {
[Column(Name = "TD"), NotNull]
public DateTime TD { get; set; }
[Column(Name = "User"), NotNull]
public string User { get; set; }
[Column(Name = "Event"), NotNull]
public string Event { get; set; }
[Column(Name = "alert")]
public int? alert { get; set; }
[PrimaryKey, Identity]
public int ID { get; set; }
}

[Table(Name = "man_act")]
public partial class man_act {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_act"), NotNull]
public int ID_act { get; set; }
[Column(Name = "ID_act_element"), NotNull]
public int ID_act_element { get; set; }
[Column(Name = "ID_man"), NotNull]
public int ID_man { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "act_exam")]
public int? act_exam { get; set; }
[Column(Name = "act_lab")]
public int? act_lab { get; set; }
[Column(Name = "act_image")]
public int? act_image { get; set; }
[Column(Name = "act_vacc")]
public int? act_vacc { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "perc")]
public int? perc { get; set; }
[Column(Name = "perc_type")]
public int? perc_type { get; set; }
[Column(Name = "perc_owner")]
public int? perc_owner { get; set; }
[Column(Name = "perc_r"), NotNull]
public int perc_r { get; set; }
[Column(Name = "perc_type_r"), NotNull]
public int perc_type_r { get; set; }
[Column(Name = "ID_user_created"), NotNull]
public int ID_user_created { get; set; }
}

[Table(Name = "man_groups")]
public partial class man_groups {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "code"), NotNull]
public int code { get; set; }
[Column(Name = "color"), NotNull]
public string color { get; set; }
[Column(Name = "use_color")]
public int? use_color { get; set; }
[Column(Name = "locked")]
public int? locked { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "exam")]
public int? exam { get; set; }
[Column(Name = "lab")]
public int? lab { get; set; }
[Column(Name = "image")]
public int? image { get; set; }
[Column(Name = "vaccine")]
public int? vaccine { get; set; }
}

[Table(Name = "man_list")]
public partial class man_list {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_group"), NotNull]
public int ID_group { get; set; }
[Column(Name = "cont"), NotNull]
public string cont { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "op"), NotNull]
public int op { get; set; }
[Column(Name = "inSh"), NotNull]
public int inSh { get; set; }
}

[Table(Name = "med_app")]
public partial class med_app {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
}

[Table(Name = "mobile_keys")]
public partial class mobile_keys {
[PrimaryKey, Identity]
public string key_value { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
}

[Table(Name = "owners")]
public partial class owners {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_status")]
public int? ID_status { get; set; }
[Column(Name = "owner_name"), NotNull]
public string owner_name { get; set; }
[Column(Name = "town"), NotNull]
public string town { get; set; }
[Column(Name = "address"), NotNull]
public string address { get; set; }
[Column(Name = "phone"), NotNull]
public string phone { get; set; }
[Column(Name = "mobile"), NotNull]
public string mobile { get; set; }
[Column(Name = "email"), NotNull]
public string email { get; set; }
[Column(Name = "region"), NotNull]
public string region { get; set; }
[Column(Name = "deleted")]
public int? deleted { get; set; }
[Column(Name = "deleted_ID_user")]
public int? deleted_ID_user { get; set; }
[Column(Name = "TD_deleted")]
public DateTime? TD_deleted { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
[Column(Name = "man_percent")]
public int? man_percent { get; set; }
[Column(Name = "discTolerance")]
public int? discTolerance { get; set; }
[Column(Name = "GDPR")]
public int? GDPR { get; set; }
[Column(Name = "tdGDPR")]
public DateTime? tdGDPR { get; set; }
[Column(Name = "signPNG"), NotNull]
public string signPNG { get; set; }
[Column(Name = "PDATA"), NotNull]
public int PDATA { get; set; }
[Column(Name = "tdPDATA")]
public DateTime? tdPDATA { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
}

[Table(Name = "patient_kinds")]
public partial class patient_kinds {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
}

[Table(Name = "patients")]
public partial class patients {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_owner"), NotNull]
public int ID_owner { get; set; }
[Column(Name = "ID_inc")]
public int? ID_inc { get; set; }
[Column(Name = "region"), NotNull]
public string region { get; set; }
[Column(Name = "ID_kind")]
public int? ID_kind { get; set; }
[Column(Name = "gender")]
public int? gender { get; set; }
[Column(Name = "birthday"), NotNull]
public string birthday { get; set; }
[Column(Name = "scars"), NotNull]
public string scars { get; set; }
[Column(Name = "ID_microchip"), NotNull]
public string ID_microchip { get; set; }
[Column(Name = "deleted")]
public int? deleted { get; set; }
[Column(Name = "deleted_ID_user")]
public int? deleted_ID_user { get; set; }
[Column(Name = "TD_deleted")]
public DateTime? TD_deleted { get; set; }
[Column(Name = "no_vaksina")]
public int? no_vaksina { get; set; }
[Column(Name = "patient_name"), NotNull]
public string patient_name { get; set; }
[Column(Name = "weight"), NotNull]
public string weight { get; set; }
[Column(Name = "NB"), NotNull]
public string NB { get; set; }
}

[Table(Name = "qr")]
public partial class qr {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "api_key"), NotNull]
public string api_key { get; set; }
[Column(Name = "TD_created")]
public DateTime? TD_created { get; set; }
[Column(Name = "assigned")]
public int? assigned { get; set; }
[Column(Name = "ID_type")]
public int? ID_type { get; set; }
[Column(Name = "ID_subj")]
public int? ID_subj { get; set; }
[Column(Name = "TD_assign")]
public DateTime? TD_assign { get; set; }
[Column(Name = "ID_user_assign")]
public int? ID_user_assign { get; set; }
[Column(Name = "h18"), NotNull]
public int h18 { get; set; }
[Column(Name = "anulated"), NotNull]
public int anulated { get; set; }
}

[Table(Name = "s_cash_receipts")]
public partial class s_cash_receipts {
[PrimaryKey, Identity]
public int receiptid { get; set; }
[Column(Name = "paymentid"), NotNull]
public int paymentid { get; set; }
[Column(Name = "receiptdatetime"), NotNull]
public DateTime receiptdatetime { get; set; }
[Column(Name = "receiptpaymenttype"), NotNull]
public int receiptpaymenttype { get; set; }
[Column(Name = "isprint"), NotNull]
public int isprint { get; set; }
[Column(Name = "userid"), NotNull]
public int userid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "document_number"), NotNull]
public string document_number { get; set; }
[Column(Name = "total_amount"), NotNull]
public decimal total_amount { get; set; }
[Column(Name = "receipt_type"), NotNull]
public int receipt_type { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "fiscal_memory_number"), NotNull]
public string fiscal_memory_number { get; set; }
[Column(Name = "cashier_fullname"), NotNull]
public string cashier_fullname { get; set; }
[Column(Name = "cashier_usernumber"), NotNull]
public int cashier_usernumber { get; set; }
[Column(Name = "cashier_username"), NotNull]
public string cashier_username { get; set; }
[Column(Name = "cashier_password"), NotNull]
public string cashier_password { get; set; }
[Column(Name = "vault_number"), NotNull]
public int vault_number { get; set; }
[Column(Name = "storno_document_number"), NotNull]
public string storno_document_number { get; set; }
[Column(Name = "storno_document_datetime")]
public DateTime? storno_document_datetime { get; set; }
[Column(Name = "storno_fiscal_memory_number"), NotNull]
public string storno_fiscal_memory_number { get; set; }
[Column(Name = "storno_reason_type")]
public int? storno_reason_type { get; set; }
}

[Table(Name = "s_cash_receipts_details")]
public partial class s_cash_receipts_details {
[PrimaryKey, Identity]
public long receipt_detail_id { get; set; }
[Column(Name = "receiptid"), NotNull]
public int receiptid { get; set; }
[Column(Name = "article_type"), NotNull]
public int article_type { get; set; }
[Column(Name = "article_name"), NotNull]
public string article_name { get; set; }
[Column(Name = "quantity"), NotNull]
public decimal quantity { get; set; }
[Column(Name = "unit"), NotNull]
public string unit { get; set; }
[Column(Name = "unit_price"), NotNull]
public decimal unit_price { get; set; }
[Column(Name = "tax_rate"), NotNull]
public int tax_rate { get; set; }
[Column(Name = "discount_amount"), NotNull]
public decimal discount_amount { get; set; }
}

[Table(Name = "s_code_types")]
public partial class s_code_types {
[PrimaryKey, Identity]
public int code_typeid { get; set; }
[Column(Name = "type_name"), NotNull]
public string type_name { get; set; }
[Column(Name = "description"), NotNull]
public string description { get; set; }
}

[Table(Name = "s_codes")]
public partial class s_codes {
[PrimaryKey, Identity]
public int codeid { get; set; }
[Column(Name = "ucode"), NotNull]
public int ucode { get; set; }
[Column(Name = "code_name"), NotNull]
public string code_name { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
[Column(Name = "code_typeid"), NotNull]
public int code_typeid { get; set; }
[Column(Name = "pos"), NotNull]
public int pos { get; set; }
[Column(Name = "vdecimal1")]
public decimal? vdecimal1 { get; set; }
[Column(Name = "parent_codeid")]
public int? parent_codeid { get; set; }
}

[Table(Name = "s_counters")]
public partial class s_counters {
[PrimaryKey, Identity]
public int counterid { get; set; }
[Column(Name = "counter_value"), NotNull]
public int counter_value { get; set; }
[Column(Name = "counter_type"), NotNull]
public string counter_type { get; set; }
[Column(Name = "counter_year"), NotNull]
public int counter_year { get; set; }
[Column(Name = "description"), NotNull]
public string description { get; set; }
}

[Table(Name = "s_durations")]
public partial class s_durations {
[PrimaryKey, Identity]
public int durationid { get; set; }
[Column(Name = "from_date"), NotNull]
public DateTime from_date { get; set; }
[Column(Name = "to_date"), NotNull]
public DateTime to_date { get; set; }
[Column(Name = "createdby_userid"), NotNull]
public int createdby_userid { get; set; }
[Column(Name = "modifiedby_userid"), NotNull]
public int modifiedby_userid { get; set; }
[Column(Name = "lastupdatetime"), NotNull]
public DateTime lastupdatetime { get; set; }
[Column(Name = "isdeleted"), NotNull]
public int isdeleted { get; set; }
}

[Table(Name = "s_info")]
public partial class s_info {
[PrimaryKey, Identity]
public int infoid { get; set; }
[Column(Name = "created_datetime"), NotNull]
public DateTime created_datetime { get; set; }
[Column(Name = "createdby_userid"), NotNull]
public int createdby_userid { get; set; }
[Column(Name = "modifiedby_userid"), NotNull]
public int modifiedby_userid { get; set; }
[Column(Name = "lastupdatetime"), NotNull]
public DateTime lastupdatetime { get; set; }
}

[Table(Name = "s_log")]
public partial class s_log {
[PrimaryKey, Identity]
public int logid { get; set; }
[Column(Name = "log_datetime"), NotNull]
public DateTime log_datetime { get; set; }
[Column(Name = "sessionid"), NotNull]
public int sessionid { get; set; }
[Column(Name = "key_id"), NotNull]
public int key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
[Column(Name = "action_type"), NotNull]
public int action_type { get; set; }
[Column(Name = "log_contain"), NotNull]
public string log_contain { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
}

[Table(Name = "s_orders")]
public partial class s_orders {
[PrimaryKey, Identity]
public int jorderid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "productid"), NotNull]
public int productid { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "measure_codeid"), NotNull]
public int measure_codeid { get; set; }
[Column(Name = "jorderdate"), NotNull]
public DateTime jorderdate { get; set; }
[Column(Name = "jorderdatetime"), NotNull]
public DateTime jorderdatetime { get; set; }
[Column(Name = "jquantity"), NotNull]
public decimal jquantity { get; set; }
[Column(Name = "jamount"), NotNull]
public decimal jamount { get; set; }
[Column(Name = "jdiscountpercent"), NotNull]
public decimal jdiscountpercent { get; set; }
[Column(Name = "discountjamount"), NotNull]
public decimal discountjamount { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
[Column(Name = "key_id")]
public int? key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
}

[Table(Name = "s_orders_annulled")]
public partial class s_orders_annulled {
[PrimaryKey, Identity]
public int annul_jorder_id { get; set; }
[Column(Name = "jorderid"), NotNull]
public int jorderid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "productid"), NotNull]
public int productid { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "measure_codeid"), NotNull]
public int measure_codeid { get; set; }
[Column(Name = "jorderdate"), NotNull]
public DateTime jorderdate { get; set; }
[Column(Name = "jorderdatetime"), NotNull]
public DateTime jorderdatetime { get; set; }
[Column(Name = "jquantity"), NotNull]
public decimal jquantity { get; set; }
[Column(Name = "jamount"), NotNull]
public decimal jamount { get; set; }
[Column(Name = "jdiscountpercent"), NotNull]
public decimal jdiscountpercent { get; set; }
[Column(Name = "discountjamount"), NotNull]
public decimal discountjamount { get; set; }
[Column(Name = "annul_datetime"), NotNull]
public DateTime annul_datetime { get; set; }
[Column(Name = "annul_userid"), NotNull]
public int annul_userid { get; set; }
}

[Table(Name = "s_paydistributions")]
public partial class s_paydistributions {
[PrimaryKey, Identity]
public int paydistributionid { get; set; }
[Column(Name = "paymentid"), NotNull]
public int paymentid { get; set; }
[Column(Name = "jorderid"), NotNull]
public int jorderid { get; set; }
[Column(Name = "distribution_datetime"), NotNull]
public DateTime distribution_datetime { get; set; }
[Column(Name = "amount"), NotNull]
public decimal amount { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
}

[Table(Name = "s_payments")]
public partial class s_payments {
[PrimaryKey, Identity]
public int paymentid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "payamount"), NotNull]
public decimal payamount { get; set; }
[Column(Name = "rest_amount"), NotNull]
public decimal rest_amount { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "paymenttype_codeid"), NotNull]
public int paymenttype_codeid { get; set; }
[Column(Name = "payment_datetime"), NotNull]
public DateTime payment_datetime { get; set; }
[Column(Name = "payment_date"), NotNull]
public DateTime payment_date { get; set; }
[Column(Name = "pattern"), NotNull]
public int pattern { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "parent_paymentid")]
public int? parent_paymentid { get; set; }
[Column(Name = "key_id")]
public int? key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
}

[Table(Name = "s_printers")]
public partial class s_printers {
[PrimaryKey, Identity]
public long printer_id { get; set; }
[Column(Name = "user_number"), NotNull]
public int user_number { get; set; }
[Column(Name = "user_name"), NotNull]
public string user_name { get; set; }
[Column(Name = "user_password"), NotNull]
public string user_password { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "sell_workplace_code"), NotNull]
public string sell_workplace_code { get; set; }
[Column(Name = "host_url"), NotNull]
public string host_url { get; set; }
[Column(Name = "host_port"), NotNull]
public int host_port { get; set; }
[Column(Name = "eik"), NotNull]
public string eik { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
}

[Table(Name = "s_product_prices")]
public partial class s_product_prices {
[PrimaryKey, Identity]
public int product_priceid { get; set; }
[Column(Name = "productid"), NotNull]
public int productid { get; set; }
[Column(Name = "unit_price"), NotNull]
public decimal unit_price { get; set; }
[Column(Name = "from_date"), NotNull]
public DateTime from_date { get; set; }
[Column(Name = "to_date"), NotNull]
public DateTime to_date { get; set; }
[Column(Name = "info_id"), NotNull]
public int info_id { get; set; }
}

[Table(Name = "s_products")]
public partial class s_products {
[PrimaryKey, Identity]
public int productid { get; set; }
[Column(Name = "ucode"), NotNull]
public int ucode { get; set; }
[Column(Name = "product_name"), NotNull]
public string product_name { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
[Column(Name = "pos"), NotNull]
public int pos { get; set; }
[Column(Name = "ismaterial"), NotNull]
public int ismaterial { get; set; }
[Column(Name = "group_codeid"), NotNull]
public int group_codeid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "key_id")]
public int? key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
}

[Table(Name = "s_sessions")]
public partial class s_sessions {
[PrimaryKey, Identity]
public int sessionid { get; set; }
[Column(Name = "userid"), NotNull]
public int userid { get; set; }
[Column(Name = "login_datetime"), NotNull]
public DateTime login_datetime { get; set; }
[Column(Name = "outlogin_datetime"), NotNull]
public DateTime outlogin_datetime { get; set; }
[Column(Name = "ip_info"), NotNull]
public string ip_info { get; set; }
}

[Table(Name = "s_settings")]
public partial class s_settings {
[PrimaryKey, Identity]
public long setting_id { get; set; }
[Column(Name = "setting_value"), NotNull]
public string setting_value { get; set; }
[Column(Name = "setting_description"), NotNull]
public string setting_description { get; set; }
}

[Table(Name = "s_supto_tables")]
public partial class s_supto_tables {
[PrimaryKey, Identity]
public int id { get; set; }
[Column(Name = "ddl_name"), NotNull]
public string ddl_name { get; set; }
[Column(Name = "ddl_type"), NotNull]
public string ddl_type { get; set; }
[Column(Name = "description"), NotNull]
public string description { get; set; }
}

[Table(Name = "s_unps")]
public partial class s_unps {
[PrimaryKey, Identity]
public int unpid { get; set; }
[Column(Name = "sysnum"), NotNull]
public int sysnum { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "open_date"), NotNull]
public DateTime open_date { get; set; }
[Column(Name = "open_datetime"), NotNull]
public DateTime open_datetime { get; set; }
[Column(Name = "close_date"), NotNull]
public DateTime close_date { get; set; }
[Column(Name = "close_datetime"), NotNull]
public DateTime close_datetime { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "statusid"), NotNull]
public int statusid { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "sell_workplace_code"), NotNull]
public string sell_workplace_code { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
}

[Table(Name = "sh_changes")]
public partial class sh_changes {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idItem"), NotNull]
public int idItem { get; set; }
[Column(Name = "ovrFrom"), NotNull]
public int ovrFrom { get; set; }
[Column(Name = "ovrTo"), NotNull]
public int ovrTo { get; set; }
[Column(Name = "tdReq"), NotNull]
public DateTime tdReq { get; set; }
[Column(Name = "idUserReq"), NotNull]
public int idUserReq { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
[Column(Name = "conf"), NotNull]
public int conf { get; set; }
[Column(Name = "tdConf")]
public DateTime? tdConf { get; set; }
[Column(Name = "idUserConf"), NotNull]
public string idUserConf { get; set; }
[Column(Name = "admNote"), NotNull]
public string admNote { get; set; }
}

[Table(Name = "sh_dnd")]
public partial class sh_dnd {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idUser"), NotNull]
public int idUser { get; set; }
[Column(Name = "tdBegin"), NotNull]
public DateTime tdBegin { get; set; }
[Column(Name = "tdEnd"), NotNull]
public DateTime tdEnd { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
}

[Table(Name = "sh_items")]
public partial class sh_items {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idLayer"), NotNull]
public int idLayer { get; set; }
[Column(Name = "idUser"), NotNull]
public int idUser { get; set; }
[Column(Name = "tdBegin")]
public DateTime? tdBegin { get; set; }
[Column(Name = "tdEnd")]
public DateTime? tdEnd { get; set; }
[Column(Name = "over"), NotNull]
public int over { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
}

[Table(Name = "sh_swaps")]
public partial class sh_swaps {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idItem1"), NotNull]
public int idItem1 { get; set; }
[Column(Name = "idItem2"), NotNull]
public int idItem2 { get; set; }
[Column(Name = "td1ask"), NotNull]
public DateTime td1ask { get; set; }
[Column(Name = "td1confirm")]
public DateTime? td1confirm { get; set; }
[Column(Name = "td2ask")]
public DateTime? td2ask { get; set; }
[Column(Name = "td2confirm")]
public DateTime? td2confirm { get; set; }
[Column(Name = "idUser1"), NotNull]
public int idUser1 { get; set; }
[Column(Name = "idUser2"), NotNull]
public int idUser2 { get; set; }
}

[Table(Name = "sheduler")]
public partial class sheduler {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "ID_user_to")]
public int? ID_user_to { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "tdEnd")]
public DateTime? tdEnd { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
[Column(Name = "TD_created")]
public DateTime? TD_created { get; set; }
[Column(Name = "ID_act_type"), NotNull]
public string ID_act_type { get; set; }
[Column(Name = "ID_user_edited")]
public int? ID_user_edited { get; set; }
[Column(Name = "TD_edited")]
public DateTime? TD_edited { get; set; }
[Column(Name = "ID_patient")]
public int? ID_patient { get; set; }
[Column(Name = "idShSpace"), NotNull]
public int idShSpace { get; set; }
[Column(Name = "idCollabs"), NotNull]
public string idCollabs { get; set; }
}

[Table(Name = "sheduler_deleted")]
public partial class sheduler_deleted {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "td"), NotNull]
public DateTime td { get; set; }
[Column(Name = "idUser"), NotNull]
public int idUser { get; set; }
[Column(Name = "cont"), NotNull]
public string cont { get; set; }
}

[Table(Name = "sheduler_man")]
public partial class sheduler_man {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idSheduler"), NotNull]
public int idSheduler { get; set; }
[Column(Name = "idMan"), NotNull]
public int idMan { get; set; }
}

[Table(Name = "sheduler_out_reasons")]
public partial class sheduler_out_reasons {
[PrimaryKey, Identity]
public int id { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "color"), NotNull]
public string color { get; set; }
[Column(Name = "descrShort"), NotNull]
public string descrShort { get; set; }
}

[Table(Name = "sheduler_spaces")]
public partial class sheduler_spaces {
[PrimaryKey, Identity]
public int id { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "color"), NotNull]
public string color { get; set; }
}

[Table(Name = "sheduler_work")]
public partial class sheduler_work {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_user"), NotNull]
public int ID_user { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
}

[Table(Name = "shop_articles")]
public partial class shop_articles {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idGroup"), NotNull]
public int idGroup { get; set; }
[Column(Name = "idSubGroup"), NotNull]
public int idSubGroup { get; set; }
[Column(Name = "idManuf"), NotNull]
public int idManuf { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "code"), NotNull]
public string code { get; set; }
[Column(Name = "barcode"), NotNull]
public string barcode { get; set; }
[Column(Name = "dimm"), NotNull]
public string dimm { get; set; }
[Column(Name = "qtyMIN"), NotNull]
public decimal qtyMIN { get; set; }
[Column(Name = "qtyOPT"), NotNull]
public decimal qtyOPT { get; set; }
[Column(Name = "qtyCURR"), NotNull]
public decimal qtyCURR { get; set; }
}

[Table(Name = "shop_docin")]
public partial class shop_docin {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idDocType"), NotNull]
public int idDocType { get; set; }
[Column(Name = "idPayType")]
public int? idPayType { get; set; }
[Column(Name = "TD_created"), NotNull]
public DateTime TD_created { get; set; }
[Column(Name = "ID_user_created"), NotNull]
public int ID_user_created { get; set; }
[Column(Name = "TD_edited"), NotNull]
public DateTime TD_edited { get; set; }
[Column(Name = "ID_user_edited"), NotNull]
public int ID_user_edited { get; set; }
[Column(Name = "idProvider"), NotNull]
public int idProvider { get; set; }
[Column(Name = "docDate"), NotNull]
public DateTime docDate { get; set; }
[Column(Name = "docNumber"), NotNull]
public string docNumber { get; set; }
[Column(Name = "VAT"), NotNull]
public int VAT { get; set; }
[Column(Name = "priceType"), NotNull]
public int priceType { get; set; }
[Column(Name = "inStore")]
public int? inStore { get; set; }
[Column(Name = "TD_instore"), NotNull]
public DateTime TD_instore { get; set; }
[Column(Name = "ID_user_instore"), NotNull]
public int ID_user_instore { get; set; }
[Column(Name = "payed")]
public int? payed { get; set; }
[Column(Name = "TD_payed")]
public DateTime? TD_payed { get; set; }
[Column(Name = "ID_user_payed")]
public int? ID_user_payed { get; set; }
[Column(Name = "isContainer")]
public int? isContainer { get; set; }
[Column(Name = "idParent")]
public int? idParent { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
[Column(Name = "payedHist")]
public int? payedHist { get; set; }
}

[Table(Name = "shop_docin_pay")]
public partial class shop_docin_pay {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idDoc"), NotNull]
public int idDoc { get; set; }
[Column(Name = "TD"), NotNull]
public DateTime TD { get; set; }
[Column(Name = "ID_user"), NotNull]
public int ID_user { get; set; }
[Column(Name = "idPayType"), NotNull]
public int idPayType { get; set; }
[Column(Name = "docDate"), NotNull]
public DateTime docDate { get; set; }
[Column(Name = "idDocType"), NotNull]
public int idDocType { get; set; }
[Column(Name = "docNumber"), NotNull]
public string docNumber { get; set; }
}

[Table(Name = "shop_docout")]
public partial class shop_docout {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idClient"), NotNull]
public int idClient { get; set; }
[Column(Name = "TD_created"), NotNull]
public DateTime TD_created { get; set; }
[Column(Name = "ID_user_created"), NotNull]
public int ID_user_created { get; set; }
[Column(Name = "TD_edited"), NotNull]
public DateTime TD_edited { get; set; }
[Column(Name = "ID_user_edited"), NotNull]
public int ID_user_edited { get; set; }
[Column(Name = "payed"), NotNull]
public int payed { get; set; }
[Column(Name = "TD_payed"), NotNull]
public DateTime TD_payed { get; set; }
[Column(Name = "ID_user_payed"), NotNull]
public int ID_user_payed { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
[Column(Name = "idDaily")]
public int? idDaily { get; set; }
[Column(Name = "idAct")]
public int? idAct { get; set; }
}

[Table(Name = "shop_group")]
public partial class shop_group {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "deleted"), NotNull]
public int deleted { get; set; }
[Column(Name = "allowNegative")]
public int? allowNegative { get; set; }
}

[Table(Name = "shop_in")]
public partial class shop_in {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idDoc"), NotNull]
public int idDoc { get; set; }
[Column(Name = "idArt"), NotNull]
public int idArt { get; set; }
[Column(Name = "qty"), NotNull]
public decimal qty { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "priceRule")]
public decimal? priceRule { get; set; }
[Column(Name = "lot"), NotNull]
public string lot { get; set; }
[Column(Name = "dateExpire")]
public DateTime? dateExpire { get; set; }
[Column(Name = "payed")]
public int? payed { get; set; }
[Column(Name = "idPartpaydoc")]
public int? idPartpaydoc { get; set; }
[Column(Name = "qtyCURR"), NotNull]
public decimal qtyCURR { get; set; }
[Column(Name = "priceTotal")]
public decimal? priceTotal { get; set; }
}

[Table(Name = "shop_manufacturers")]
public partial class shop_manufacturers {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
}

[Table(Name = "shop_out")]
public partial class shop_out {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idDoc"), NotNull]
public int idDoc { get; set; }
[Column(Name = "idArt"), NotNull]
public int idArt { get; set; }
[Column(Name = "qty"), NotNull]
public decimal qty { get; set; }
[Column(Name = "price"), NotNull]
public decimal price { get; set; }
[Column(Name = "qtyCURR"), NotNull]
public decimal qtyCURR { get; set; }
[Column(Name = "priceTotal")]
public decimal? priceTotal { get; set; }
}

[Table(Name = "shop_price")]
public partial class shop_price {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idArticle"), NotNull]
public int idArticle { get; set; }
[Column(Name = "idProvider"), NotNull]
public int idProvider { get; set; }
[Column(Name = "priceManufVAT"), NotNull]
public decimal priceManufVAT { get; set; }
[Column(Name = "priceOffVAT"), NotNull]
public decimal priceOffVAT { get; set; }
[Column(Name = "priceEndVAT"), NotNull]
public decimal priceEndVAT { get; set; }
[Column(Name = "priceManuf"), NotNull]
public decimal priceManuf { get; set; }
[Column(Name = "priceOff"), NotNull]
public decimal priceOff { get; set; }
[Column(Name = "priceEnd"), NotNull]
public decimal priceEnd { get; set; }
[Column(Name = "perc1"), NotNull]
public decimal perc1 { get; set; }
[Column(Name = "perc2"), NotNull]
public decimal perc2 { get; set; }
}

[Table(Name = "shop_subgroup")]
public partial class shop_subgroup {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idGroup"), NotNull]
public int idGroup { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "deleted"), NotNull]
public int deleted { get; set; }
}

[Table(Name = "sms_buffer")]
public partial class sms_buffer {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_Patient"), NotNull]
public string ID_Patient { get; set; }
[Column(Name = "ID_Vaksina"), NotNull]
public string ID_Vaksina { get; set; }
[Column(Name = "repeat_days"), NotNull]
public string repeat_days { get; set; }
[Column(Name = "Vaksina_type"), NotNull]
public string Vaksina_type { get; set; }
[Column(Name = "Name_sobstvenik"), NotNull]
public string Name_sobstvenik { get; set; }
[Column(Name = "Mobile"), NotNull]
public string Mobile { get; set; }
[Column(Name = "email"), NotNull]
public string email { get; set; }
[Column(Name = "ID_client"), NotNull]
public string ID_client { get; set; }
[Column(Name = "ID_inc"), NotNull]
public string ID_inc { get; set; }
}

[Table(Name = "sms_logger")]
public partial class sms_logger {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "user"), NotNull]
public string user { get; set; }
[Column(Name = "action"), NotNull]
public string action { get; set; }
[Column(Name = "TD"), NotNull]
public DateTime TD { get; set; }
}

[Table(Name = "store_articles")]
public partial class store_articles {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_group"), NotNull]
public int ID_group { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "dimm"), NotNull]
public string dimm { get; set; }
[Column(Name = "price_out")]
public decimal? price_out { get; set; }
[Column(Name = "price_in")]
public decimal? price_in { get; set; }
[Column(Name = "qty")]
public decimal? qty { get; set; }
[Column(Name = "min_qty")]
public decimal? min_qty { get; set; }
[Column(Name = "use_min_qty")]
public int? use_min_qty { get; set; }
[Column(Name = "deleted")]
public int? deleted { get; set; }
[Column(Name = "marker")]
public int? marker { get; set; }
[Column(Name = "nonMaterial"), NotNull]
public int nonMaterial { get; set; }
}

[Table(Name = "store_groups")]
public partial class store_groups {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "descr"), NotNull]
public string descr { get; set; }
[Column(Name = "hidden")]
public int? hidden { get; set; }
[Column(Name = "vaccine")]
public int? vaccine { get; set; }
[Column(Name = "shop")]
public int? shop { get; set; }
[Column(Name = "hospital")]
public int? hospital { get; set; }
[Column(Name = "cabinet")]
public int? cabinet { get; set; }
[Column(Name = "exam")]
public int? exam { get; set; }
[Column(Name = "image")]
public int? image { get; set; }
[Column(Name = "lab")]
public int? lab { get; set; }
[Column(Name = "sorter")]
public int? sorter { get; set; }
[Column(Name = "system")]
public int? system { get; set; }
[Column(Name = "allowNegative")]
public int? allowNegative { get; set; }
}

[Table(Name = "store_in")]
public partial class store_in {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_art"), NotNull]
public int ID_art { get; set; }
[Column(Name = "ID_provider"), NotNull]
public int ID_provider { get; set; }
[Column(Name = "date_invoice")]
public DateTime? date_invoice { get; set; }
[Column(Name = "invoice_number"), NotNull]
public string invoice_number { get; set; }
[Column(Name = "lot"), NotNull]
public string lot { get; set; }
[Column(Name = "date_expire")]
public DateTime? date_expire { get; set; }
[Column(Name = "qty")]
public decimal? qty { get; set; }
[Column(Name = "price")]
public decimal? price { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "qty_used")]
public int? qty_used { get; set; }
[Column(Name = "loss")]
public int? loss { get; set; }
[Column(Name = "TD_loss")]
public DateTime? TD_loss { get; set; }
[Column(Name = "ID_user_loss")]
public int? ID_user_loss { get; set; }
[Column(Name = "priceTotal")]
public decimal? priceTotal { get; set; }
}

[Table(Name = "store_out")]
public partial class store_out {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_art"), NotNull]
public int ID_art { get; set; }
[Column(Name = "ID_store_in"), NotNull]
public string ID_store_in { get; set; }
[Column(Name = "TD")]
public DateTime? TD { get; set; }
[Column(Name = "ID_user")]
public int? ID_user { get; set; }
[Column(Name = "qty")]
public decimal? qty { get; set; }
[Column(Name = "ID_act")]
public int? ID_act { get; set; }
[Column(Name = "ID_hosp")]
public int? ID_hosp { get; set; }
[Column(Name = "ID_med_app")]
public int? ID_med_app { get; set; }
[Column(Name = "price")]
public decimal? price { get; set; }
[Column(Name = "act_exam")]
public int? act_exam { get; set; }
[Column(Name = "act_lab")]
public int? act_lab { get; set; }
[Column(Name = "act_image")]
public int? act_image { get; set; }
[Column(Name = "act_vacc")]
public int? act_vacc { get; set; }
[Column(Name = "ID_act_element")]
public int? ID_act_element { get; set; }
[Column(Name = "quick")]
public int? quick { get; set; }
[Column(Name = "system")]
public int? system { get; set; }
[Column(Name = "app_note"), NotNull]
public string app_note { get; set; }
[Column(Name = "priceTotal")]
public decimal? priceTotal { get; set; }
}

[Table(Name = "store_providers")]
public partial class store_providers {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "name"), NotNull]
public string name { get; set; }
[Column(Name = "email"), NotNull]
public string email { get; set; }
[Column(Name = "phone"), NotNull]
public string phone { get; set; }
[Column(Name = "perc1"), NotNull]
public decimal perc1 { get; set; }
[Column(Name = "perc2")]
public decimal? perc2 { get; set; }
}

[Table(Name = "sync_delete")]
public partial class sync_delete {
[Column(Name = "tblName"), NotNull]
public string tblName { get; set; }
[Column(Name = "idRecord")]
public int? idRecord { get; set; }
}

[Table(Name = "sync_insert")]
public partial class sync_insert {
[PrimaryKey, Identity]
public string tblName { get; set; }
}

[Table(Name = "sync_update")]
public partial class sync_update {
[Column(Name = "tblName"), NotNull]
public string tblName { get; set; }
[Column(Name = "idRecord")]
public int? idRecord { get; set; }
}

[Table(Name = "system")]
public partial class system {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "cont"), NotNull]
public string cont { get; set; }
[Column(Name = "int_descr"), NotNull]
public string int_descr { get; set; }
}

[Table(Name = "system_rest_days")]
public partial class system_rest_days {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "date")]
public DateTime? date { get; set; }
[Column(Name = "note"), NotNull]
public string note { get; set; }
}

[Table(Name = "urls")]
public partial class urls {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "idAct"), NotNull]
public int idAct { get; set; }
[Column(Name = "idImaging"), NotNull]
public int idImaging { get; set; }
[Column(Name = "type"), NotNull]
public int type { get; set; }
}

[Table(Name = "users")]
public partial class users {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "username"), NotNull]
public string username { get; set; }
[Column(Name = "password"), NotNull]
public string password { get; set; }
[Column(Name = "ID_auth")]
public int? ID_auth { get; set; }
[Column(Name = "full_name"), NotNull]
public string full_name { get; set; }
[Column(Name = "out_")]
public int? out_ { get; set; }
[Column(Name = "del")]
public int? del { get; set; }
[Column(Name = "email"), NotNull]
public string email { get; set; }
[Column(Name = "phone"), NotNull]
public string phone { get; set; }
[Column(Name = "notify_email")]
public int? notify_email { get; set; }
[Column(Name = "notify_sms")]
public int? notify_sms { get; set; }
[Column(Name = "color"), NotNull]
public string color { get; set; }
[Column(Name = "in_charge"), NotNull]
public int in_charge { get; set; }
[Column(Name = "isStoreAdmin"), NotNull]
public int isStoreAdmin { get; set; }
[Column(Name = "isShopSeller"), NotNull]
public int isShopSeller { get; set; }
[Column(Name = "isShopPriceEditor"), NotNull]
public int isShopPriceEditor { get; set; }
[Column(Name = "isShopPriceViewer"), NotNull]
public int isShopPriceViewer { get; set; }
[Column(Name = "isShopArtEditor"), NotNull]
public int isShopArtEditor { get; set; }
[Column(Name = "isShopInEditor"), NotNull]
public int isShopInEditor { get; set; }
[Column(Name = "isShedulerOwn"), NotNull]
public int isShedulerOwn { get; set; }
[Column(Name = "isShedulerAll"), NotNull]
public int isShedulerAll { get; set; }
[Column(Name = "shDoc"), NotNull]
public int shDoc { get; set; }
[Column(Name = "shTech"), NotNull]
public int shTech { get; set; }
[Column(Name = "shHosp"), NotNull]
public int shHosp { get; set; }
[Column(Name = "shHospTech"), NotNull]
public int shHospTech { get; set; }
[Column(Name = "shShop"), NotNull]
public int shShop { get; set; }
[Column(Name = "shLab"), NotNull]
public int shLab { get; set; }
[Column(Name = "shRec"), NotNull]
public int shRec { get; set; }
[Column(Name = "hrsPLeave"), NotNull]
public int hrsPLeave { get; set; }
[Column(Name = "hrsULeave"), NotNull]
public int hrsULeave { get; set; }
[Column(Name = "hrsLearn"), NotNull]
public int hrsLearn { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
}

[Table(Name = "vacc_repeat")]
public partial class vacc_repeat {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_vacc"), NotNull]
public int ID_vacc { get; set; }
[Column(Name = "rep_days"), NotNull]
public int rep_days { get; set; }
}

[Table(Name = "wallets")]
public partial class wallets {
[PrimaryKey, Identity]
public int ID { get; set; }
[Column(Name = "ID_user"), NotNull]
public int ID_user { get; set; }
[Column(Name = "curr_sum")]
public float? curr_sum { get; set; }
}

[Table(Name = "vs_cash_receipts")]
public partial class vs_cash_receipts {
[Column(Name = "receiptid"), NotNull]
public int receiptid { get; set; }
[Column(Name = "receiptdatetime"), NotNull]
public DateTime receiptdatetime { get; set; }
[Column(Name = "receipt_type_name"), NotNull]
public string receipt_type_name { get; set; }
[Column(Name = "owner_name"), NotNull]
public string owner_name { get; set; }
[Column(Name = "paymentid"), NotNull]
public int paymentid { get; set; }
[Column(Name = "payamount"), NotNull]
public decimal payamount { get; set; }
[Column(Name = "article_type"), NotNull]
public int article_type { get; set; }
[Column(Name = "article_name"), NotNull]
public string article_name { get; set; }
[Column(Name = "quantity"), NotNull]
public decimal quantity { get; set; }
[Column(Name = "unit"), NotNull]
public string unit { get; set; }
[Column(Name = "unit_price"), NotNull]
public decimal unit_price { get; set; }
[Column(Name = "tax_rate"), NotNull]
public int tax_rate { get; set; }
[Column(Name = "discount_amount"), NotNull]
public decimal discount_amount { get; set; }
[Column(Name = "payment_type"), NotNull]
public string payment_type { get; set; }
[Column(Name = "receiptpaymenttype"), NotNull]
public int receiptpaymenttype { get; set; }
[Column(Name = "isprint"), NotNull]
public int isprint { get; set; }
[Column(Name = "userid"), NotNull]
public int userid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "document_number"), NotNull]
public string document_number { get; set; }
[Column(Name = "receipt_type"), NotNull]
public int receipt_type { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "fiscal_memory_number"), NotNull]
public string fiscal_memory_number { get; set; }
[Column(Name = "cashier_fullname"), NotNull]
public string cashier_fullname { get; set; }
[Column(Name = "cashier_usernumber"), NotNull]
public int cashier_usernumber { get; set; }
[Column(Name = "cashier_username"), NotNull]
public string cashier_username { get; set; }
[Column(Name = "cashier_password"), NotNull]
public string cashier_password { get; set; }
[Column(Name = "vault_number"), NotNull]
public int vault_number { get; set; }
[Column(Name = "storno_document_number"), NotNull]
public string storno_document_number { get; set; }
[Column(Name = "storno_document_datetime")]
public DateTime? storno_document_datetime { get; set; }
[Column(Name = "storno_fiscal_memory_number"), NotNull]
public string storno_fiscal_memory_number { get; set; }
[Column(Name = "receipt_detail_id"), NotNull]
public long receipt_detail_id { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "rest_amount"), NotNull]
public decimal rest_amount { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "paymenttype_codeid"), NotNull]
public int paymenttype_codeid { get; set; }
[Column(Name = "payment_datetime"), NotNull]
public DateTime payment_datetime { get; set; }
[Column(Name = "payment_date"), NotNull]
public DateTime payment_date { get; set; }
[Column(Name = "pattern"), NotNull]
public int pattern { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "parent_paymentid")]
public int? parent_paymentid { get; set; }
}

[Table(Name = "vs_codes")]
public partial class vs_codes {
[Column(Name = "codeid"), NotNull]
public int codeid { get; set; }
[Column(Name = "ucode"), NotNull]
public int ucode { get; set; }
[Column(Name = "code_name"), NotNull]
public string code_name { get; set; }
[Column(Name = "type_name"), NotNull]
public string type_name { get; set; }
[Column(Name = "description"), NotNull]
public string description { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
[Column(Name = "code_typeid"), NotNull]
public int code_typeid { get; set; }
[Column(Name = "pos"), NotNull]
public int pos { get; set; }
[Column(Name = "vdecimal1")]
public decimal? vdecimal1 { get; set; }
[Column(Name = "parent_codeid")]
public int? parent_codeid { get; set; }
}

[Table(Name = "vs_orders")]
public partial class vs_orders {
[Column(Name = "jorderid"), NotNull]
public int jorderid { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "sysnum"), NotNull]
public int sysnum { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "open_date"), NotNull]
public DateTime open_date { get; set; }
[Column(Name = "open_datetime"), NotNull]
public DateTime open_datetime { get; set; }
[Column(Name = "sell_workplace_code"), NotNull]
public string sell_workplace_code { get; set; }
[Column(Name = "product_ucode"), NotNull]
public int product_ucode { get; set; }
[Column(Name = "product_name"), NotNull]
public string product_name { get; set; }
[Column(Name = "user_code"), NotNull]
public int user_code { get; set; }
[Column(Name = "out_discount_out_vat_amount")]
public decimal? out_discount_out_vat_amount { get; set; }
[Column(Name = "out_discount_out_vat_unit_price")]
public decimal? out_discount_out_vat_unit_price { get; set; }
[Column(Name = "discountjamount"), NotNull]
public decimal discountjamount { get; set; }
[Column(Name = "vat_percent")]
public decimal? vat_percent { get; set; }
[Column(Name = "vat_amount")]
public decimal? vat_amount { get; set; }
[Column(Name = "jamount"), NotNull]
public decimal jamount { get; set; }
[Column(Name = "jquantity"), NotNull]
public decimal jquantity { get; set; }
[Column(Name = "close_date"), NotNull]
public DateTime close_date { get; set; }
[Column(Name = "close_datetime"), NotNull]
public DateTime close_datetime { get; set; }
[Column(Name = "jpartner_code"), NotNull]
public int jpartner_code { get; set; }
[Column(Name = "jpartnername"), NotNull]
public string jpartnername { get; set; }
[Column(Name = "createdby_userid"), NotNull]
public int createdby_userid { get; set; }
[Column(Name = "usn_status"), NotNull]
public string usn_status { get; set; }
}

[Table(Name = "vs_orders_annulled")]
public partial class vs_orders_annulled {
[Column(Name = "annul_jorder_id"), NotNull]
public int annul_jorder_id { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "sysnum"), NotNull]
public int sysnum { get; set; }
[Column(Name = "open_date"), NotNull]
public DateTime open_date { get; set; }
[Column(Name = "open_datetime"), NotNull]
public DateTime open_datetime { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "sell_workplace_code"), NotNull]
public string sell_workplace_code { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "product_ucode"), NotNull]
public int product_ucode { get; set; }
[Column(Name = "product_name"), NotNull]
public string product_name { get; set; }
[Column(Name = "jquantity"), NotNull]
public decimal jquantity { get; set; }
[Column(Name = "out_discount_out_vat_unit_price")]
public decimal? out_discount_out_vat_unit_price { get; set; }
[Column(Name = "discountjamount"), NotNull]
public decimal discountjamount { get; set; }
[Column(Name = "vat_percent")]
public decimal? vat_percent { get; set; }
[Column(Name = "vat_amount")]
public decimal? vat_amount { get; set; }
[Column(Name = "jamount"), NotNull]
public decimal jamount { get; set; }
[Column(Name = "annul_date")]
public DateTime? annul_date { get; set; }
[Column(Name = "annul_datetime"), NotNull]
public DateTime annul_datetime { get; set; }
[Column(Name = "annul_userid"), NotNull]
public int annul_userid { get; set; }
}

[Table(Name = "vs_paydistributions")]
public partial class vs_paydistributions {
[Column(Name = "paydistributionid"), NotNull]
public int paydistributionid { get; set; }
[Column(Name = "paymentid"), NotNull]
public int paymentid { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "sysnum"), NotNull]
public int sysnum { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "sell_workplace_code"), NotNull]
public string sell_workplace_code { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "close_date"), NotNull]
public DateTime close_date { get; set; }
[Column(Name = "close_datetime"), NotNull]
public DateTime close_datetime { get; set; }
[Column(Name = "ucode"), NotNull]
public int ucode { get; set; }
[Column(Name = "productid"), NotNull]
public int productid { get; set; }
[Column(Name = "product_name"), NotNull]
public string product_name { get; set; }
[Column(Name = "jquantity"), NotNull]
public decimal jquantity { get; set; }
[Column(Name = "measure_codeid"), NotNull]
public int measure_codeid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "measure_name"), NotNull]
public string measure_name { get; set; }
[Column(Name = "unit_price_jamount")]
public decimal? unit_price_jamount { get; set; }
[Column(Name = "out_vat_unit_price")]
public decimal? out_vat_unit_price { get; set; }
[Column(Name = "discountjamount"), NotNull]
public decimal discountjamount { get; set; }
[Column(Name = "vat_percent")]
public decimal? vat_percent { get; set; }
[Column(Name = "vat_amount")]
public decimal? vat_amount { get; set; }
[Column(Name = "jamount"), NotNull]
public decimal jamount { get; set; }
[Column(Name = "pattern"), NotNull]
public int pattern { get; set; }
[Column(Name = "payment_date"), NotNull]
public DateTime payment_date { get; set; }
[Column(Name = "payment_datetime"), NotNull]
public DateTime payment_datetime { get; set; }
[Column(Name = "receiptdatetime")]
public DateTime? receiptdatetime { get; set; }
[Column(Name = "receipt_serial_number"), NotNull]
public string receipt_serial_number { get; set; }
[Column(Name = "createdby_userid"), NotNull]
public int createdby_userid { get; set; }
}

[Table(Name = "vs_payments")]
public partial class vs_payments {
[Column(Name = "paymentid"), NotNull]
public int paymentid { get; set; }
[Column(Name = "unpid"), NotNull]
public int unpid { get; set; }
[Column(Name = "payamount"), NotNull]
public decimal payamount { get; set; }
[Column(Name = "rest_amount"), NotNull]
public decimal rest_amount { get; set; }
[Column(Name = "clientid"), NotNull]
public int clientid { get; set; }
[Column(Name = "paymenttype_codeid"), NotNull]
public int paymenttype_codeid { get; set; }
[Column(Name = "payment_datetime"), NotNull]
public DateTime payment_datetime { get; set; }
[Column(Name = "payment_date"), NotNull]
public DateTime payment_date { get; set; }
[Column(Name = "pattern"), NotNull]
public int pattern { get; set; }
[Column(Name = "infoid"), NotNull]
public int infoid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "key_id")]
public int? key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
[Column(Name = "is_advance"), NotNull]
public int is_advance { get; set; }
[Column(Name = "owner_name"), NotNull]
public string owner_name { get; set; }
[Column(Name = "mobile"), NotNull]
public string mobile { get; set; }
[Column(Name = "unp"), NotNull]
public string unp { get; set; }
[Column(Name = "sysnum"), NotNull]
public int sysnum { get; set; }
[Column(Name = "open_date"), NotNull]
public DateTime open_date { get; set; }
[Column(Name = "close_date"), NotNull]
public DateTime close_date { get; set; }
[Column(Name = "sell_point_code"), NotNull]
public string sell_point_code { get; set; }
[Column(Name = "sell_point_name"), NotNull]
public string sell_point_name { get; set; }
[Column(Name = "serial_number"), NotNull]
public string serial_number { get; set; }
[Column(Name = "createdby_userid"), NotNull]
public int createdby_userid { get; set; }
[Column(Name = "payment_type"), NotNull]
public string payment_type { get; set; }
[Column(Name = "out_vat_amount")]
public decimal? out_vat_amount { get; set; }
[Column(Name = "vat_amount")]
public decimal? vat_amount { get; set; }
}

[Table(Name = "vs_products")]
public partial class vs_products {
[Column(Name = "productid"), NotNull]
public int productid { get; set; }
[Column(Name = "ucode"), NotNull]
public int ucode { get; set; }
[Column(Name = "product_name"), NotNull]
public string product_name { get; set; }
[Column(Name = "durationid"), NotNull]
public int durationid { get; set; }
[Column(Name = "pos"), NotNull]
public int pos { get; set; }
[Column(Name = "ismaterial"), NotNull]
public int ismaterial { get; set; }
[Column(Name = "group_codeid"), NotNull]
public int group_codeid { get; set; }
[Column(Name = "vat_codeid"), NotNull]
public int vat_codeid { get; set; }
[Column(Name = "current_price")]
public decimal? current_price { get; set; }
[Column(Name = "from_date"), NotNull]
public DateTime from_date { get; set; }
[Column(Name = "to_date"), NotNull]
public DateTime to_date { get; set; }
[Column(Name = "lastupdatetime"), NotNull]
public DateTime lastupdatetime { get; set; }
[Column(Name = "isdeleted"), NotNull]
public int isdeleted { get; set; }
[Column(Name = "key_id")]
public int? key_id { get; set; }
[Column(Name = "key_name"), NotNull]
public string key_name { get; set; }
}

[Table(Name = "vs_users")]
public partial class vs_users {
[Column(Name = "ID"), NotNull]
public int ID { get; set; }
[Column(Name = "full_name"), NotNull]
public string full_name { get; set; }
[Column(Name = "from_date"), NotNull]
public DateTime from_date { get; set; }
[Column(Name = "to_date"), NotNull]
public DateTime to_date { get; set; }
[Column(Name = "lastupdatetime"), NotNull]
public DateTime lastupdatetime { get; set; }
[Column(Name = "del")]
public int? del { get; set; }
[Column(Name = "has_access"), NotNull]
public string has_access { get; set; }
[Column(Name = "user_privileges"), NotNull]
public string user_privileges { get; set; }
}

}
