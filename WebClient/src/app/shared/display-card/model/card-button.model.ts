import { ColorClassEnum, IconsEnum } from "src/app/services/enum-cluster.service";

export interface CardButtonModel {
    text: string;
    icon: IconsEnum | undefined;
    colorClass: ColorClassEnum;

    disabled?: boolean;

    onClick: (identifier: string | undefined) => void;
}